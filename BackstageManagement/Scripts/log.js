layui.use(['form', 'laydate', 'table', 'jquery'], function () {
    var form = layui.form;
    var laydate = layui.laydate;
    var table = layui.table;
    var $ = layui.jquery;
    var day = new Date();
    var daterange=day.getFullYear()+"-"+(day.getMonth()+1)+"-"+day.getDate()+" ~ "+day.getFullYear()+"-"+(day.getMonth()+1)+"-"+day.getDate();
    var logdate=laydate.render({
        elem: '#date',
        range: '~',
        lang: 'en',
        value:daterange,
        done: function (value,date,endDate) {
            daterange = value;
        }
    })
    var tableindex = table.render({
        elem: "#logtable",
        url: '/Log/GetLogInfo',
        method: 'post',
        where: {
            system: $("#system").val(),
            logtype: $("#logtype").val(),
            daterange: daterange,
            condition: $("#condition").val(),
        },
        height: 'full-220',
        cols: [[
            { field: '', type: 'numbers', title: '序号', sort: false, width: 80 },
            { field: 'BelongSystemString', title: '所属系统', templet:"#button1", sort: false },
            { field: 'LogTypeString', title: '日志类型',templet:'#button2', sort: false },
            { field: 'LoginNo', title: '工号', sort: false },
            { field: 'LogFunction', title: '日志功能', sort: false },
            { field: 'LogContent', title: '日志内容', sort: false },
            { field: 'CreationTimeString', title: '创建时间', sort: true },
            { field: '', title: '操作', templet: '#operationTpl' }
        ]],
        done: function (res, curr, count) {
            if (res.code == 1) {
                layer.alert(res.msg);
            }
            else if (res.code == 2) {
                window.location = res.redirect;
            }
        },
        page: true
    });
    table.on('tool(logtable)', function (obj) {
        var data = obj.data;
        if (obj.event == 'detail') {
            var temindex = layer.open({
                type: 1,
                area: ['600px', '570px'],
                title: '日志详情',
                content: $('#details').html(),
                success: function (layero, index) {
                    $("#belongSystemString").val(data.BelongSystemString);
                    $("#logTypeString").val(data.LogTypeString);
                    $("#loginNo").val(data.LoginNo);
                    $("#logFunction").val(data.LogFunction);
                    $("#creationTimeString").val(data.CreationTimeString);
                    $("#logContent").val(data.LogContent);
                },
            })
        }
    })
    $("#btnSearch").click(function () {
        tableindex.reload({
            url: '/Log/GetLogInfo',
            method: 'post',
            where: {
                system: $("#system").val(),
                logtype: $("#logtype").val(),
                daterange: daterange,
                condition: $("#condition").val(),
            },
        });
    })
     
})