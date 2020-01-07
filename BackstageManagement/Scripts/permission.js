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
            { field: '', type: 'numbers', sort: false},
            { field: 'Name', title: '菜单名称' },
            { field: 'Id', title: 'ID', sort: false, hide: true },
            { field: 'ParentName', title: '父级菜单', sort: false },
            { field: 'Icon', title: 'Icon', sort: false },
            { field: 'Url', title: '菜单地址', sort: false },
            { field: 'Remark', title: '备注', sort: false },
            { field: '', title: '类型', templet:'#typeTpl', sort: false },
            { field: 'CreationTimeString', title: '创建时间', sort: true },
            { field: '', title: '操作', templet: '#operationTpl' }
        ]],
        done: function (res, curr, count) {
            if (res.code === 1) {
                layer.alert(res.msg);
            }
            NProgress.done();
        },
        page: true
    });
    table.on('tool(permissiontable)', function (obj) {
        var data = obj.data;
        if (obj.event === 'delete') {
            layer.confirm("确定要删除吗？", function (index) {
                $.ajax({
                    url: '/Permission/DeletePermission',
                    data: {
                        Id: data.Id
                    },
                    type: 'Post',
                    success: function (res) {
                        if (res.code === 0) {
                            tableindex.reload();
                            console.log(res);
                        }
                        else {
                            layer.alert(res.msg);
                        }
                    }
                });
                layer.close(index);

            });
        }
        else if (obj.event === 'edit') {
            var temindex = layer.open({
                type: 1,
                area: ['700px', '460px'],
                title: '编辑菜单',
                content: $('#permission').html(),
                success: function (layero, index) {
                    layero.addClass('layui-form');
                    layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                    $.ajax({
                        url: '/Permission/QueryPermissionInfo',
                        type: 'Post',
                        dataType: 'json',
                        success: function (res) {
                            if (res.code === 0) {
                                $.each(res.data, function (index, item) {
                                    $('#parentName').append(new Option(item.Name, item.Id));
                                });
                                if (data.Type === 1) {
                                    $("input[title='菜单']").attr("checked", true);
                                    $("input[title='按钮']").attr("checked", false);
                                }
                                else {
                                    $("input[title='菜单']").attr("checked", false);
                                    $("input[title='按钮']").attr("checked", true);
                                }
                                $("#name").val(data.Name);
                                $("#url").val(data.Url);
                                $("#parentName").val(data.ParentId);
                                $("#remark").val(data.Remark);
                                $("#icon").val(data.Icon);
                            }
                            else {
                                layer.close(temindex);
                                layer.alert(res.msg);
                            }
                            form.render();
                        }
                    });

                },
                btn: ['保存', '取消'],
                yes: function (index, layero) {
                    form.on("submit(formVerify)", function () {
                        var permissionName = $("#name").val();
                        var permissionUrl = $("#url").val();
                        var parentId = $("#parentName").val();
                        var type = $("input[name='type']:checked").val();
                        var icon = $("#icon").val();
                        var remark = $("#remark").val();
                        var id = data.Id;
                        var json = {};
                        json.name = permissionName;
                        json.url = permissionUrl;
                        json.parentId = parentId;
                        json.type = type;
                        json.icon = icon;
                        json.remark = remark;
                        json.id = id;
                        $.ajax({
                            url: '/Permission/EditPermissionInfo',
                            data: {
                                param: JSON.stringify(json)
                            },
                            type: 'Post',

                            success: function (res) {
                                if (res.code === 0) {
                                    layer.close(temindex);
                                    tableindex.reload();
                                    console.log(res);
                                }
                                else if (res.code === 2) {
                                    window.location = res.redirect;
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
    $("#btnadd").click(function () {
        var temindex = layer.open({
            type: 1,
            area: ['700px', '460px'],
            title: '添加菜单',
            content: $('#permission').html(),
            btn: ['保存', '取消'],
            success: function (layero, index) {
                layero.addClass('layui-form');
                layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                $.ajax({
                    url: '/Permission/QueryPermissionInfo',
                    type: 'Post',
                    dataType: 'json',
                    success: function (res) {
                        if (res.code === 0) {
                            $.each(res.data, function (index, item) {
                                $('#parentName').append(new Option(item.Name, item.Id));
                            });
                        }
                        else {
                            layer.close(temindex);
                            layer.alert(res.msg);
                        }
                        form.render();
                    }
                });

            },
            yes: function (index, layero) {
                form.on('submit(formVerify)', function () {
                    var permissionName = $("#name").val();
                    var permissionUrl = $("#url").val();
                    var parentId = $("#parentName").val();
                    var type = $("input[name='type']:checked").val();
                    var icon = $("#icon").val();
                    var remark = $("#remark").val();
                    var json = {};
                    json.name = permissionName;
                    json.url = permissionUrl;
                    json.parentId = parentId;
                    json.type = type;
                    json.icon = icon;
                    json.remark = remark;
                    $.ajax({
                        url: '/Permission/AddPermissionInfo',
                        data: {
                            param: JSON.stringify(json)
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