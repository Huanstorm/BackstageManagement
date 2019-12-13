layui.use(['form','jquery','table','upload'], function () {
    var form = layui.form;
    var $ = layui.jquery;
    var table = layui.table;
    var upload = layui.upload;

    var tableindex = table.render({
        elem: "#UNTLogtable",
        url: '/UNTLog/GetUNTLogInfo',
        toolbar:"#tabletoolbar",
        method: 'post',
        height: 'full-220',
        //toolbar: true,
        cols: [[
            { field: '', type: 'numbers' },
            {field:'',type:'checkbox'},
            { field: 'UNTMaterialNo', title: '物料号', sort: false, width:150  },
            { field: 'Id', title: 'Id', sort: false,hide:true },
            { field: 'UNTSN', title: 'SN号', sort: false, width: 200 },
            { field: 'LoginNo', title: '工号', sort: false, width: 150 },
            { field: 'Description', title: '描述', sort: false },
            { field: 'CreationTimeString', title: '创建时间', sort: true,width:180 },
            { field: '', title: '操作', templet: '#operationTpl',width:150 }
        ]],
        done: function (res, curr, count) {
            console.log(res)
        },
        page: true
    });
    var uploadindex = upload.render({
        elem: '#upload',
        url: '/UNTLog/UploadUNTLog',
        accept: 'file',
        //exts: 'xml|Xml',

        done: function (res) {
            console.log(res);
            if (res.code == 0) {
                tableindex.reload();
                layer.alert("上传成功");
            }
            else {
                layer.alert("上传失败，" + res.msg);
            }

        },
    })
    table.on("toolbar(UNTLogtable)", function (obj) {
        var checkStatus = table.checkStatus(obj.config.id);
        switch (obj.event) {
            case 'download':
                var data = checkStatus.data.Id;
                layer.alert(JSON.stringify(data));
        }
    })
    table.on('tool(UNTLogtable)', function (obj) {
        var data = obj.data;
        if (obj.event == 'delete') {
            layer.confirm("确定要删除吗？", function (index) {
                $.ajax({
                    url: '/UNTLog/DeleteUNTLog',
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
        else if (obj.event == 'download') {
            var $eleform = $("<form method='post'></form>");
            $eleform.attr("action", "/UNTLog/DownLoadUNTLog?Id=" + data.Id);
            $(document.body).append($eleform);
            $eleform.submit();

        }
    })

    $("#search").click(function () {
        tableindex.reload({
            url: '/UNTLog/GetUNTLogInfo',
            method: 'post',
            where: {
                condition: $("#condition").val(),
            },
        });
    })
})