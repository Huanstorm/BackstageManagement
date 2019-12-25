
layui.use(["table", "jquery", "tree", "layer"], function () {
    NProgress.start();
    var table = layui.table;
    var $ = layui.jquery;
    var tree = layui.tree;
    var layer = layui.layer;
    var tableindex = table.render({
        elem: "#user",
        url: '/User/QueryAdminUserInfo',
        method: 'post',
        skin: 'nob',
        //size:'lg',
        cols: [[
            { field: 'Id', title: 'LoginId', hide: true },
            { field: 'LoginNo', title: '用户', sort: false },
        ]],
        done: function (res, curr, count) {
            $('th').hide();
            $('.layui-table-header').css({ "border-width": "0 0 0px" });
        }
    })
    
    $.ajax({
        url: '/User_Permission/QueryPermissionInfoById',
        data: {
            employeeId: 0
        },
        type: 'POST',
        dataType: 'json',
        success: function (res) {
            if (res.code == 0) {
                tree.render({
                    elem: '#tree',
                    showCheckbox: true,
                    id: 'permissionTree',
                    data: res.data
                })
                
            }
            NProgress.done();
        }
    })
    var employeeId = -1;
    table.on("row(user)", function (obj) {
        employeeId=obj.data.Id
        $.ajax({
            url: '/User_Permission/QueryPermissionInfoById',
            data: {
                employeeId: employeeId
            },
            type: 'POST',
            dataType:'json',
            success: function (res) {
                if (res.code==0) {
                    tree.reload('permissionTree', {
                        data: res.data
                    })
                }
                else if (res.code == 2) {
                    window.location = res.redirect;
                }
                else {
                    layer.alert(res.msg);
                }
            }
        })
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    })
    $("#btnSave").click(function () {
        var checkData = tree.getChecked("permissionTree");
        if (employeeId == 16) {
            layer.alert("无权修改该账户权限。");
            return;
        }

        $.ajax({
            url: '/User_Permission/SavePermissionInfoByLoginNo',
            data: {
                employeeId: employeeId,
                checkedNode: JSON.stringify(checkData)
            },
            type: 'post',
            success: function (res) {
                if (res.code == 0) {
                    $.ajax({
                        url: '/User_Permission/QueryPermissionInfoById',
                        data: {
                            employeeId: employeeId
                        },
                        type: 'POST',
                        dataType: 'json',
                        success: function (res) {
                            if (res.code == 0) {
                                tree.reload('permissionTree', {
                                    data: res.data
                                })
                                
                            }
                        }
                    })
                    layer.msg("保存成功。");
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
    

})
