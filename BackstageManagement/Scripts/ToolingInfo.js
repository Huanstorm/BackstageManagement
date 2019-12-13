layui.use(['form', 'table', 'layer', 'jquery'], function () {
    var form = layui.form;
    var table = layui.table;
    var layer = layui.layer;
    var $ = layui.jquery;
    

    var tableindex = table.render({
        elem: "#toolingInfotable",
        url: '/ToolingInfo/GetToolingInfo',
        method: 'post',
        where: { condition: $("#condition").val()},
        height: 'full-210',
        cols: [[
            { field: '', type: 'numbers', title: '序号', sort: false, width: 100 },
            { field: 'Id', title: 'Id', hide: true },
            { field: 'ToolSNNo', title: '工装SN', sort: false },
            { field: 'ToolMaterialNo', title: '工装物料号', sort: false },
            { field: 'ConnUseNum', title: '连接器使用次数', sort: false },
            { field: 'CableUseNum', title: '电缆线使用次数', sort: false },
            { field: 'Remark', title: '备注', sort: false },
            { field: 'CreationTimeString', title: '创建时间', sort: true },
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
        },
        page: true
    });

    $("#btnSearch").click(function () {
        tableindex.reload({
            url: '/ToolingInfo/GetToolingInfo',
            method: 'post',
            where: {
                condition: $("#condition").val()
            },
        });
    })

    table.on('tool(toolingInfotable)', function (obj) {
        var data = obj.data;
        if (obj.event == 'delete') {
            layer.confirm("确定要删除吗？", function (index) {
                $.ajax({
                    url: '/ToolingInfo/DeleteToolingInfo',
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
                area: ['700px', '400px'],
                title: '编辑工装信息',
                content: $('#addToolingInfo').html(),
                success: function (layero, index) {
                    layero.addClass('layui-form');
                    layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                    $("#toolSNNo").val(data.ToolSNNo);
                    $("#connUseNum").val(data.ConnUseNum);
                    $("#cableUseNum").val(data.CableUseNum);
                    $("#toolMaterialNo").val(data.ToolMaterialNo);
                    $("#remark").val(data.Remark);
                    form.render();

                },
                btn: ['保存', '取消'],
                yes: function (index, layero) {
                    form.on('submit(formVerify)', function () {
                        var toolSNNo = $("#toolSNNo").val();
                        var connUseNum = $("#connUseNum").val();
                        var cableUseNum = $("#cableUseNum").val();
                        var toolMaterialNo = $("#toolMaterialNo").val();
                        var remark = $("#remark").val();
                        var entity = {};
                        entity.id = data.Id;
                        entity.toolSNNo = toolSNNo;
                        entity.connUseNum = connUseNum;
                        entity.cableUseNum = cableUseNum;
                        entity.toolMaterialNo = toolMaterialNo;
                        entity.remark = remark;
                        $.ajax({
                            url: '/ToolingInfo/EditToolingInfo',
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
    $("#btnRefreshToolingInfo").click(function () {
        tableindex.reload();
    })

    $("#btnAddToolingInfo").click(function () {
        var temindex = layer.open({
            type: 1,
            area: ['700px', '400px'],
            title: '增加工装信息',
            content: $('#addToolingInfo').html(),
            success: function (layero, index) {


                document.getElementById("ConnUseNum").readOnly = true;

                document.getElementById("CableUseNum").readOnly = true;


            },
            btn: ['保存', '取消'],
            success: function (layero,index) {
                layero.addClass('layui-form');
                layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                form.render();
            },
            yes: function (index, layero) {

                form.on('submit(formVerify)', function () {
                    var toolSNNo = $("#toolSNNo").val();
                    var connUseNum = $("#connUseNum").val();
                    var cableUseNum = $("#cableUseNum").val();
                    var toolMaterialNo = $("#toolMaterialNo").val();
                    var remark = $("#remark").val();
                    var entity = {};
                    entity.toolSNNo = toolSNNo;
                    entity.connUseNum = connUseNum;
                    entity.cableUseNum = cableUseNum;
                    entity.toolMaterialNo = toolMaterialNo;
                    entity.remark = remark;
                    $.ajax({
                        url: '/ToolingInfo/AddToolingInfo',
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
})