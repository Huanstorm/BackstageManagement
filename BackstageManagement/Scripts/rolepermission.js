
layui.use(["table", "jquery", "tree", "layer"], function () {
    NProgress.start();
    var table = layui.table;
    var $ = layui.jquery;
    var tree = layui.tree;
    var layer = layui.layer;
    var tableindex = table.render({
        elem: "#role",
        url: '/Role/GetRoles',
        method: 'post',
        skin: 'nob',
        //size:'lg',
        cols: [[
            { field: 'Id', hide: true },
            { field: 'Name', title: '角色', sort: false }
        ]],
        done: function (res, curr, count) {
            $('th').hide();
            $('.layui-table-header').css({ "border-width": "0 0 0px" });
        }
    });

    $.ajax({
        url: '/RolePermission/QueryPermissionInfoById',
        data: {
            roleId: 0
        },
        type: 'POST',
        dataType: 'json',
        success: function (res) {
            if (res.code === 0) {
                tree.render({
                    elem: '#tree',
                    showCheckbox: true,
                    id: 'permissionTree',
                    data: res.data
                });
            }
            NProgress.done();
        }
    });
    var roleId = -1;
    table.on("row(role)", function (obj) {
        var loadIndex= layer.load();
        roleId = obj.data.Id;
        $.ajax({
            url: '/RolePermission/QueryPermissionInfoById',
            data: {
                roleId: roleId
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.code === 0) {
                    tree.reload('permissionTree', {
                        data: res.data
                    });
                }
                else {
                    layer.alert(res.msg);
                }
                layer.close(loadIndex);
            }
        });
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    });
    $("#btnSave").click(function () {
        var checkData = tree.getChecked("permissionTree");
        var loadindex = layer.load();
        $.ajax({
            url: '/RolePermission/SavePermissionInfoByLoginNo',
            data: {
                roleId: roleId,
                checkedNode: JSON.stringify(checkData)
            },
            type: 'post',
            success: function (res) {
                if (res.code === 0) {
                    $.ajax({
                        url: '/RolePermission/QueryPermissionInfoById',
                        data: {
                            roleId: roleId
                        },
                        type: 'POST',
                        dataType: 'json',
                        success: function (res) {
                            if (res.code === 0) {
                                tree.reload('permissionTree', {
                                    data: res.data
                                });
                                layer.close(loadindex);
                                layer.msg("保存成功");
                                console.log(res);
                            }
                            else {
                                layer.close(loadindex);
                                layer.alert(res.msg);
                                
                            }
                        }
                    });
                }
                else {
                    layer.close(loadindex);
                    layer.alert(res.msg);
                }
            }
        });
    });
});
