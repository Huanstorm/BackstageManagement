layui.use(['form', 'table', 'layer', 'jquery'], function () {
    NProgress.start();
    var form = layui.form;
    var table = layui.table;
    var layer = layui.layer;
    var $ = layui.jquery;
    var tableindex = table.render({
        elem: "#usertable",
        url: '/User/GetUserInfo',
        method: 'post',
        height: 'full-210',
        cols: [[
            { field: '', type: 'numbers', title: '序号', sort: false, width: 100 },
            { field: 'Id', title: 'LoginId', hide: true },
            { field: 'LoginNo', title: '工号', sort: false },
            { field: 'LoginName', title: '姓名', sort: false },
            { field: 'EmployeeTypeString', title: '员工类型', sort: false },
            { field: 'Remark', title: '备注', sort: false },
            { field: 'ModifyTimeString', title: '修改时间', sort: true },
            { field: '', title: '操作', templet: '#operationTpl' }
        ]],
        done: function (res, curr, count) {
            console.log(res)
            if (res.code == 1) {
                layer.alert(res.msg);
            }
            else if (res.code == 2) {
                window.location = res.redirect;
            }
            NProgress.done();
        },
        page: true
    });
    table.on('tool(usertable)', function (obj) {
        var data = obj.data;
        if (data.LoginNo=="admin") {
            layer.alert("admin用户不能编辑或删除！");
            return;
        }
        if (obj.event == 'delete') {
            layer.confirm("确定要删除该用户？", function (index) {
                $.ajax({
                    url: '/User/DeleteUser',
                    data: {
                        Id: data.Id
                    },
                    type: 'Post',
                    success: function (res) {
                        if (res.code == 0) {
                            layer.close(index);
                            tableindex.reload();
                            console.log(res);
                        }
                        else if (res.code == 2) {
                            window.location = res.redirect;
                        }
                        else{
                            layer.alert(res.msg);
                        }
                    }
                })
                layer.close(index);

            })
        }
        else if (obj.event == 'edit') {
            var temindex = layer.open({
                type: 1,
                area: ['700px', '400px'],
                title: '编辑员工信息',
                content: $('#addUser').html(),
                success: function (layero, index) {
                    layero.addClass('layui-form');
                    layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');

                    $("#loginNo").val(data.LoginNo);
                    $("#loginName").val(data.LoginName);
                    $("#employeeType").val(data.EmployeeType);
                    $("#remark").val(data.Remark);
                    $("#psd").hide();
                    if (data.EmployeeType == 1) {
                        
                        $("#psd").show();
                        $("#password").val(data.Password);
                    }
                    else {
                        layero.find("#password").removeAttr("required").removeAttr("lay-verify");
                    }

                    form.render();

                },
                btn: ['保存', '取消'],
                yes: function (index, layero) {
                    form.on('submit(formVerify)', function () {
                        var loginNo = $("#loginNo").val();
                        var loginName = $("#loginName").val();
                        var employeeType = $("#employeeType").val();
                        var password = $("#password").val();
                        var remark = $("#remark").val();
                        var entity = {};
                        entity.Id = data.Id;
                        entity.loginNo = loginNo;
                        entity.loginName = loginName;
                        entity.employeeType = employeeType;
                        entity.password = password;
                        entity.remark = remark;
                        $.ajax({
                            url: '/User/EditUserInfo',
                            data: {
                                param: JSON.stringify(entity)
                            },
                            type: 'Post',
                            success: function (res) {
                                if (res.code == 0) {
                                    layer.close(temindex);
                                    tableindex.reload();
                                    console.log(res);
                                }
                                else if (res.code == 2) {
                                    window.location = res.redirect;
                                }
                                else {
                                    layer.alert(res.msg);
                                }
                            }
                        })
                    })
                },
            })
        }
    })
    $("#btnRefresh").click(function () {
        tableindex.reload();
    })
    $("#btnAddUser").click(function () {
        var temindex = layer.open({
            type: 1,
            area: ['700px', '400px'],
            title: '添加员工信息',
            content: $('#addUser').html(),
            btn: ['保存', '取消'],
            success: function (layero, index) {
                layero.addClass('layui-form');
                layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                
                form.render();
            },
            yes: function (index, layero) {
                form.on('submit(formVerify)', function () {
                    var entity = {};
                    var loginNo = $("#loginNo").val();
                    var loginName = $("#loginName").val();
                    var employeeType = $("#employeeType").val();
                    var password = $("#password").val();
                    var remark = $("#remark").val();
                    var entity = {};
                    entity["LoginNo"] = loginNo;
                    entity["LoginName"] = loginName;
                    entity["EmployeeType"] = employeeType;
                    entity["Password"] = password;
                    entity["Remark"] = remark;
                    $.ajax({
                        url: '/User/AddUserInfo',
                        data: {
                            param: JSON.stringify(entity)
                        },
                        type: 'Post',
                        success: function (res) {
                            if (res.code == 0) {
                                layer.close(temindex);
                                tableindex.reload();
                                console.log(res);
                            }
                            else if (res.code == 2) {
                                window.location = res.redirect;
                            }
                            else {
                                layer.alert(res.msg);
                            }
                        }
                    })
                })
            },
        })



        form.render('select');

    })

    form.on('select(usertype)', function (data) {
        if (data.value == 1) {
            $("#psd").show();
            $("#password").val("");
            $("#password").attr("required", true).attr("lay-verify","required");
        }
        else {
            $("#psd").hide();
            $("#password").val("");
            $("#password").removeAttr("required").removeAttr("lay-verify");
        }
    })
})