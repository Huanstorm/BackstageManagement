layui.use(['form', 'table', 'layer', 'jquery'], function () {
    var form = layui.form;
    var table = layui.table;
    var layer = layui.layer;
    var $ = layui.jquery;
    

    var tableindex = table.render({
        elem: "#infoConfigtable",
        url: '/InfoConfig/GetInfoConfig',
        method: 'post',
        height: 'full-210',
        cols: [[
            { field: '', type: 'numbers', title: '序号', sort: false, width: 100 },
            { field: 'Id', title: 'Id', hide: true },
            { field: 'KeyName', title: 'Key', sort: false },
            { field: 'Value', title: 'Value', sort: false },
            { field: 'Description', title: '描述', sort: false },
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
            url: '/InfoConfig/GetInfoConfig',
            method: 'post',
            where: {
                KeyName: $("#key").val()
            },
        });
    })

    table.on('tool(infoConfigtable)', function (obj) {
        var data = obj.data;
        if (obj.event == 'delete') {
            layer.confirm("确定要删除吗？",  function (index) {
                $.ajax({
                    url: '/InfoConfig/DeleteInfoConfig',
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
                title: '编辑配置信息',
                content: $('#addInfoConfig').html(),
                success: function (layero, index) {
                    layero.addClass('layui-form');
                    layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                    
                    $("#keyName").val(data.KeyName);
                    document.getElementById("keyName").readOnly = true;
                    $("#value").val(data.Value);
                    $("#description").val(data.Description);

                    form.render();

                },
                btn: ['保存', '取消'],
                yes: function (index, layero) {
                    form.on('submit(formVerify)', function () {
                        var keyName = $("#keyName").val();
                        var value = $("#value").val();
                        var description = $("#description").val();
                        var json = {};
                        json.id =data.Id;
                        json.keyName = keyName;
                        json.value = value;
                        json.description = description;
                        $.ajax({
                            url: '/InfoConfig/EditInfoConfig',
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

    $("#btnAddInfoConfig").click(function () {
        var temindex = layer.open({
            type: 1,
            area: ['680px', '430px'],
            title: '添加配置信息',
            content: $('#addInfoConfig').html(),
            btn: ['保存', '取消'],
            success: function (layero,index) {
                layero.addClass('layui-form');
                layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                form.render();
            },
            yes: function (index, layero) {
                form.on('submit(formVerify)', function () {
                    var keyName = $("#keyName").val();
                    var value = $("#value").val();
                    var description = $("#description").val();
                    var json = {};
                    json.keyName = keyName;
                    json.value = value;
                    json.description = description;
                    $.ajax({
                        url: '/InfoConfig/AddInfoConfig',
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
    })
})