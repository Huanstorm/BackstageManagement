layui.use(['form', 'table', 'layer', 'jquery'], function () {
    var form = layui.form;
    var table = layui.table;
    var layer = layui.layer;
    var $ = layui.jquery;
    

    var tableindex = table.render({
        elem: "#workStationtable",
        url: '/WorkStation/GetWorkStationInfo',
        method: 'post',
        where: { condition: $("#condition").val() },
        height: 'full-210',
        cols: [[
            { field: '', type: 'numbers', title: '编号', sort: false, width: 100 },
            { field: 'Id', title: 'Id', hide: true },
            { field: 'WorkStationNo', title: '工作站编号', sort: false },
            { field: 'WorkStationName', title: '工作站名称', sort: false },
            { field: 'WorkStationDescription', title: '描述', sort: false },
            { field: 'Remark', title: '备注', sort: false },
            { field: 'CreationTimeString', title: '创建时间', sort: true },
            { field: '', title: '操作', templet: '#operationTpl' }
        ]],
        done: function (res, curr, count) {
            console.log(res)
        },
        page: true
    });

    $("#btnSearch").click(function () {
        tableindex.reload({
            url: '/WorkStation/GetWorkStationInfo',
            method: 'post',
            where: {
                condition: $("#condition").val()
            },
        });
    })

    table.on('tool(workStationtable)', function (obj) {
        var data = obj.data;
        if (obj.event == 'delete') {
            layer.confirm("确定要删除吗？", function (index) {
                $.ajax({
                    url: '/WorkStation/DeleteWorkStation',
                    data: {
                        id: data.Id
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
                area: ['680px', '430px'],
                title: '编辑工作站信息',
                content: $('#addWorkStation').html(),
                success: function (layero, index) {
                    layero.addClass('layui-form');
                    layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                    $("#workStationNo").val(data.WorkStationNo);
                    $("#workStationName").val(data.WorkStationName);
                    $("#workStationDescription").val(data.WorkStationDescription);
                    $("#remark").val(data.Remark);
                    form.render();
                },
                btn: ['保存', '取消'],
                yes: function (index, layero) {
                    form.on('submit(formVerify)', function () {
                        var workStationNo = $("#workStationNo").val();
                        var workStationName = $("#workStationName").val();
                        var workStationDescription = $("#workStationDescription").val();
                        var remark = $("#remark").val();
                        var json = {};
                        json.id = data.Id;
                        json.workStationNo = workStationNo;
                        json.workStationName = workStationName;
                        json.workStationDescription = workStationDescription;
                        json.remark = remark;
                        $.ajax({
                            url: '/WorkStation/EditWorkStationInfo',
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

    $("#btnAddWorkStation").click(function () {
        var temindex = layer.open({
            type: 1,
            area: ['700px', '400px'],
            title: '添加工作站信息',
            content: $('#addWorkStation').html(),
            btn: ['保存', '取消'],
            success: function (layero, index) {
                layero.addClass('layui-form');
                layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');

                form.render();
            },
            yes: function (index, layero) {
                form.on('submit(formVerify)', function () {
                    var workStationNo = $("#workStationNo").val();
                    var workStationName = $("#workStationName").val();
                    var workStationDescription = $("#workStationDescription").val();
                    var remark = $("#remark").val();
                    var json = {};
                    json.workStationNo = workStationNo;
                    json.workStationName = workStationName;
                    json.workStationDescription = workStationDescription;
                    json.remark = remark;
                    $.ajax({
                        url: '/WorkStation/AddWorkStationInfo',
                        data: {
                            param:JSON.stringify(json)
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