layui.use(['form','jquery','table','upload'], function () {
    var form = layui.form;
    var $ = layui.jquery;
    var table = layui.table;
    var upload=layui.upload;
    var tableindex;
    tableindex=table.render({
        elem: "#scripttable",
        url: '/UNTScript/GetUNTScriptInfo',
        method: 'post',
        height: 'full-210',
        toolbar: true,
        cols: [[
            { field: '', type: 'numbers', title: '序号', sort: false, width: 80 },
            { field: 'ScriptNo', title: '脚本号', sort: false },
            { field: 'Id', title: 'Id', sort: false, hide: true },
            { field: 'ScriptName', title: '脚本名称', sort: false },
            { field: 'IsMultiCalBox', title: '分离式开关', templet:'#switchTpl', sort: false },
            { field: 'CalBoxAType', title: 'CalBox-A', sort: false },
            { field: 'CalBoxBType', title: 'CalBox-B', sort: false },
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
    
    table.on('tool(scripttable)', function (obj) {
        var data = obj.data;
        
        if (obj.event == 'delete') {
            layer.confirm("确定要删除吗？",function (index) {
                $.ajax({
                    url: '/UNTScript/DeleteUNTScript',
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
        else if (obj.event == 'detail') {
            
            var temindex = layer.open({
                type: 1,
                area: ['630px', '700px'],
                title: '被测件脚本详情',
                content: $('#details').html(),
                success: function (layero, index) {
                    var loadIndex = layer.load();
                    $.ajax({
                        url: "/UNTScript/QueryScriptById",
                        type:'post',
                        data: {id:data.Id},
                        success: function (res) {
                            $("#scriptNo").val(res.data.ScriptNo);
                            $("#scriptName").val(res.data.ScriptName);
                            $("#isMultiCalBox").val(res.data.IsMultiCalBox);
                            $("#calBoxAType").val(res.data.CalBoxAType);
                            $("#calBoxBType").val(res.data.CalBoxBType);
                            $("#creationTimeString").val(res.data.CreationTimeString);
                            $("#scriptContentString").val(res.data.ScriptContentString);
                            layer.close(loadIndex);
                        }
                    })
                },
            })
            
        }
    })
    $("#search").click(function () {
        tableindex.reload({
            url: '/UNTScript/GetUNTScriptInfo',
            method: 'post',
            where: {
                condition: $("#condition").val(),
            },
        });
    })
    var uploadIndex;
    $("#btnAdd").click(function () {
        var temindex = layer.open({
            type: 1,
            area: ['600px', '600px'],
            title: '添加脚本',
            content: $('#untScript').html(),
            btn: ['保存', '取消'],
            success: function (layero, index) {
                layero.addClass('layui-form');
                layero.find('.layui-layer-btn0').attr('lay-filter', 'formVerify').attr('lay-submit', '');
                layero.find("#calBoxBType").removeAttr("required").removeAttr("lay-verify");
                $("#calBoxB").hide();
                form.render();
                uploadIndex=upload.render({
                    elem: '#choose',
                    url: '/UNTScript/UploadScript',
                    auto: false,
                    bindAction: '#upload',
                    accept: 'file',
                    exts: 'xml|Xml',
                    done: function (res, index) {
                        console.log(res);
                        console.log(index);
                        if (res.code == 0) {
                            tableindex.reload();
                            layer.alert("上传成功");
                        }
                        else if (res.code == 1) {
                            layer.alert(res.msg);
                        }
                        else if (res.code == 2) {
                            window.location = res.redirect;
                        }
                    },
                })
                form.on('switch(isMultiCalBox)', function (data) {
                    if (data.elem.checked) {
                        layero.find("#calBoxBType").attr("required", true).attr("lay-verify", "required");
                        $("#calBoxB").show();
                    }
                    else {
                        layero.find("#calBoxBType").removeAttr("required").removeAttr("lay-verify");
                        $("#calBoxB").hide();
                        $("#calBoxBType").val("");
                    }
                })
                
            },
            yes: function (index, layero) {
                form.on('submit(formVerify)', function () {
                    var entity = {};
                    var scriptNo = $("#scriptNo").val();
                    var scriptName = $("#scriptName").val();
                    var isMultiCalBox = $("input[name=isMultiCalBox]").is(":checked");
                    var calBoxAType = $("#calBoxAType").val();
                    var calBoxBType = $("#calBoxBType").val();
                    var entity = {};
                    entity.scriptNo = scriptNo;
                    entity.scriptName = scriptName;
                    entity.isMultiCalBox = isMultiCalBox;
                    entity.calBoxAType = calBoxAType;
                    entity.calBoxBType = calBoxBType;
                    uploadIndex.reload({
                        data: {
                            param: JSON.stringify(entity)
                        },
                        done: function (res, index, upload) {
                            if (res.code==0) {
                                layer.close(temindex);
                                tableindex.reload();
                                console.log(res);
                            }
                            else {
                                layer.alert(res.msg);
                            }
                        }
                    })
                    $("#upload").click();
                    //$.ajax({
                    //    url: '/UNTScript/AddUNTScript',
                    //    data: {
                    //        param: JSON.stringify(entity)
                    //    },
                    //    type: 'Post',
                    //    success: function (res) {
                    //        if (res.code == 0) {
                    //            //layer.close(temindex);
                    //            //tableindex.reload();
                    //            //console.log(res);
                    //            $("#upload").click();
                    //        }
                    //        else if (res.code == 2) {
                    //            window.location = res.redirect;
                    //        }
                    //        else {
                    //            layer.alert(res.msg);
                    //        }
                    //    }
                    //})
                })
            },
        })
    })
})