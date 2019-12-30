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
            { field: '', type: 'numbers', sort: false },
            { field: 'Id', hide: true },
            { field: 'LoginName', title: '登录名', sort: false },
            { field: 'RealName', title: '昵称', sort: false },
            { field: 'RoleName', title: '角色', templet: '#roleTpl', sort: false },
            { field: 'CreationTimeString', title: '创建时间', sort: true },
            { field: 'Remark', title: '备注', sort: false },
            { field: '', title: '操作', templet: '#operationTpl' }
        ]],
        done: function (res, curr, count) {
            if (res.code !== 0) {
                layer.alert(res.msg);
            }
            NProgress.done();
        },
        page: true
    });
    table.on('tool(usertable)', function (obj) {
        var data = obj.data;
        if (data.LoginNo === "admin") {
            layer.alert("admin用户不能编辑或删除！");
            return;
        }
        if (obj.event === 'delete') {
            layer.confirm("确定要删除该用户？", function (index) {
                $.ajax({
                    url: '/User/DeleteUser',
                    data: {
                        Id: data.Id
                    },
                    type: 'Post',
                    success: function (res) {
                        if (res.code === 0) {
                            layer.close(index);
                            tableindex.reload();
                        }
                        else {
                            layer.close(index);
                            layer.alert(res.msg);
                        }
                    }
                });
            });
        }
        else if (obj.event === 'edit') {
            var temindex = layer.open({
                type: 1,
                area: ['680px', '400px'],
                title: '编辑员工信息',
                content: $('#userInfo').html(),
                success: function (layero, index) {
                    layero.addClass('layui-form');
                    layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                    $.ajax({
                        url: '/Role/GetRoles',
                        type: 'post',
                        dataType: 'json',
                        success: function (res) {
                            if (res.code === 0) {
                                $.each(res.data, function (index, item) {
                                    $('#userRole').append(new Option(item.Name, item.Id));
                                });
                            }
                            else {
                                layer.alert(res.msg);
                            }
                            $("#loginName").val(data.LoginName);
                            $("#realName").val(data.RealName);
                            $("#userRole").val(data.RoleId);
                            $("#remark").val(data.Remark);
                            $("#password").val(data.Password);

                            form.render();
                        }
                    });
                    

                },
                btn: ['保存', '取消'],
                yes: function (index, layero) {
                    form.on('submit(formVerify)', function () {
                        var entity = {};
                        var loginName = $("#loginName").val();
                        var realName = $("#realName").val();
                        var password = $("#password").val();
                        var roleId = $("#userRole").val();
                        var remark = $("#remark").val();
                        entity.Id = data.Id;
                        entity.loginName = loginName;
                        entity.realName = realName;
                        entity.password = password;
                        entity.roleId = roleId;
                        entity.remark = remark;
                        $.ajax({
                            url: '/User/EditUserInfo',
                            data: {
                                param: JSON.stringify(entity)
                            },
                            type: 'Post',
                            success: function (res) {
                                if (res.code === 0) {
                                    layer.close(temindex);
                                    tableindex.reload();
                                }
                                else {
                                    layer.alert(res.msg);
                                }
                            }
                        });
                    });
                }
            });
        }
    });
    $("#btnRefresh").click(function () {
        tableindex.reload();
    });
    $("#btnAddUser").click(function () {
        var temindex = layer.open({
            type: 1,
            area: ['680px', '400px'],
            title: '新增用户',
            content: $('#userInfo').html(),
            btn: ['保存', '取消'],
            success: function (layero, index) {
                layero.addClass('layui-form');
                layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                $.ajax({
                    url: '/Role/GetRoles',
                    type: 'post',
                    dataType: 'json',
                    success: function (res) {
                        if (res.code === 0) {
                            $.each(res.data, function (index, item) {
                                $('#userRole').append(new Option(item.Name, item.Id));
                            });
                        }
                        else {
                            layer.alert(res.msg);
                        }
                        form.render();
                    }
                });
            },
            yes: function (index, layero) {
                form.on('submit(formVerify)', function () {
                    var entity = {};
                    var loginName = $("#loginName").val();
                    var realName = $("#realName").val();
                    var password = $("#password").val();
                    var roleId = $("#userRole").val();
                    var remark = $("#remark").val();
                    entity.loginName = loginName;
                    entity.realName = realName;
                    entity.password = password;
                    entity.roleId = roleId;
                    entity.remark = remark;
                    $.ajax({
                        url: '/User/AddUserInfo',
                        data: {
                            param: JSON.stringify(entity)
                        },
                        type: 'Post',
                        success: function (res) {
                            if (res.code === 0) {
                                layer.close(temindex);
                                tableindex.reload();
                            }
                            else {
                                layer.alert(res.msg);
                            }
                        }
                    });
                });
            }
        });
    });
});