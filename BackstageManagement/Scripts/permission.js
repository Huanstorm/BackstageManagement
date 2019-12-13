layui.use(['form', 'table', 'layer', 'jquery'], function () {
    NProgress.start();
    var form = layui.form;
    var table = layui.table;
    var layer = layui.layer;
    var $ = layui.jquery;
    var tableindex = table.render({
        elem: "#permissiontable",
        url: '/Permission/GetPermissionInfo',
        method: 'post',
        height: 'full-210',
        cols: [[
            { field: '', type: 'numbers', title: '序号', sort: false, width: 100 },
            { field: 'PermissionName', title: '菜单名称', },
            { field: 'ParentId', title: '父级菜单ID', sort: false,hide:true },
            { field: 'ParentName', title: '父级菜单', sort: false,  },
            { field: 'PermissionUrl', title: '菜单地址', sort: false },
            { field: 'PermissionDescription', title: '菜单描述', sort: false },
            { field: 'Remark', title: '备注', sort: false },
            { field: 'ModifyTimeString', title: '修改时间', sort: true },
            { field: '', title: '操作', templet: '#operationTpl' }
        ]],
        done: function (res, curr, count) {
            console.log(res)
            if (res.code == 2) {
                window.location = res.redirect;
            }
            else if (res.code==1) {
                layer.alert(res.msg);
            }
            NProgress.done();
        },
        page: true
    });
    table.on('tool(permissiontable)', function (obj) {
        var data = obj.data;
        if (obj.event == 'delete') {
            layer.confirm("确定要删除吗？", function (index) {
                $.ajax({
                    url: '/Permission/DeletePermission',
                    data: {
                        Id: data.Id
                    },
                    type: 'Post',
                    success: function (res) {
                        if (res.code == 0) {
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
                layer.close(index);

            })
        }
        else if (obj.event == 'edit') {
            var temindex = layer.open({
                type: 1,
                area: ['700px', '500px'],
                title: '编辑菜单',
                content: $('#addPermission').html(),
                success: function (layero, index) {
                    layero.addClass('layui-form');
                    layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                    $.ajax({
                        url: '/Permission/QueryPermissionInfo',
                        type: 'Post',
                        dataType: 'json',
                        success: function (res) {
                            if (res.code==0) {
                                $.each(res.data, function (index, item) {
                                    $('#parentPermissionName').append(new Option(item.PermissionName, item.Id));
                                })
                                $("#permissionName").val(data.PermissionName);
                                $("#permissionUrl").val(data.PermissionUrl);
                                $("#parentPermissionName").val(data.ParentId);
                                $("#permissionDescription").val(data.PermissionDescription);
                                $("#remark").val(data.Remark);
                            }
                            else {
                                layer.close(temindex);
                                layer.alert(res.msg);
                            }
                        }
                    })
                    form.render();
                },
                btn: ['保存', '取消'],
                yes: function (index, layero) {
                    form.on("submit(formVerify)", function () {
                        var permissionName = $("#permissionName").val();
                        var permissionUrl = $("#permissionUrl").val();
                        var parentId = $("#parentPermissionName").val();
                        var permissionDescription = $("#permissionDescription").val();
                        var remark = $("#remark").val();
                        var json = {};
                        json.id = data.Id;
                        json.permissionName = permissionName;
                        json.permissionUrl = permissionUrl;
                        json.parentId = parentId;
                        json.permissionDescription = permissionDescription;
                        json.remark = remark
                        $.ajax({
                            url: '/Permission/EditPermissionInfo',
                            data: {
                                param: JSON.stringify(json)
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
    $("#btnAddPermission").click(function () {
        var temindex = layer.open({
            type: 1,
            area: ['700px', '500px'],
            title: '添加菜单',
            content: $('#addPermission').html(),
            btn: ['保存', '取消'],
            success: function (layero,index) {
                layero.addClass('layui-form');
                layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                $.ajax({
                    url: '/Permission/QueryPermissionInfo',
                    type: 'Post',
                    dataType: 'json',
                    success: function (res) {
                        if (res.code==0) {
                            $.each(res.data, function (index, item) {
                                $('#parentPermissionName').append(new Option(item.PermissionName, item.Id));
                            })
                            form.render('select');
                        }
                        else {
                            layer.close(temindex);
                            layer.alert(res.msg);
                        }
                    }
                })
                form.render();
            },
            yes: function (index, layero) {
                form.on('submit(formVerify)', function () {
                    var permissionName = $("#permissionName").val();
                    var permissionUrl = $("#permissionUrl").val();
                    var parentId = $("#parentPermissionName").val();
                    var permissionDescription = $("#permissionDescription").val();
                    var remark = $("#remark").val();
                    var json = {};
                    json.permissionName = permissionName;
                    json.permissionUrl = permissionUrl;
                    json.parentId = parentId;
                    json.permissionDescription = permissionDescription;
                    json.remark = remark;
                    $.ajax({
                        url: '/Permission/AddPermissionInfo',
                        data: {
                            param: JSON.stringify(json)
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
})