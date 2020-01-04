layui.use(['form', 'table', 'layer', 'jquery'], function () {
    NProgress.start();
    var form = layui.form;
    var table = layui.table;
    var layer = layui.layer;
    var $ = layui.jquery;
    var tableindex = table.render({
        elem: "#roletable",
        url: '/Role/GetRolesForList',
        method: 'post',
        height: 'full-210',
        cols: [[
            { field: '', type: 'numbers', sort: false },
            { field: 'Id', hide: true },
            { field: 'Name', title: '角色名', sort: false },
            { field: 'IsEnable', title: '激活', templet: '#isEnabledTpl', sort: false },
            { field: 'Remark', title: '备注', sort: false },
            { field: 'CreationTimeString', title: '创建时间', sort: true },
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
    table.on('tool(roletable)', function (obj) {
        var data = obj.data;
        if (obj.event === 'delete') {
            layer.confirm("确定要删除该角色？", function (index) {
                $.ajax({
                    url: '/Role/DeleteRole',
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
                content: $('#roleInfo').html(),
                success: function (layero, index) {
                    layero.addClass('layui-form');
                    layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                    $("#roleName").val(data.Name);
                    if (data.IsEnabled) {
                        $("#isEnabled").attr("checked", true);
                    }
                    else {
                        $("#isEnabled").attr("checked", false);
                    }
                    $("#remark").val(data.Remark);
                    form.render();
                },
                btn: ['保存', '取消'],
                yes: function (index, layero) {
                    form.on('submit(formVerify)', function () {
                        var entity = {};
                        var name = $("#roleName").val();
                        var isEnabled = $("#isEnabled").is(":checked");
                        var remark = $("#remark").val();
                        entity.id = data.Id;
                        entity.name = name;
                        entity.isEnabled = isEnabled;
                        entity.remark = remark;
                        $.ajax({
                            url: '/Role/EditRole',
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
    $("#btnAdd").click(function () {
        var temindex = layer.open({
            type: 1,
            area: ['680px', '400px'],
            title: '新增角色',
            content: $('#roleInfo').html(),
            btn: ['保存', '取消'],
            success: function (layero, index) {
                layero.addClass('layui-form');
                layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                form.render();
            },
            yes: function (index, layero) {
                form.on('submit(formVerify)', function () {
                    var entity = {};
                    var name = $("#roleName").val();
                    var isEnabled = $("#isEnabled").is(":checked");
                    var remark = $("#remark").val();
                    entity.name = name;
                    entity.isEnabled = isEnabled;
                    entity.remark = remark;
                    $.ajax({
                        url: '/Role/AddRole',
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