layui.use(['form', 'laydate', 'table', 'jquery'], function () {
    NProgress.start();
    var form = layui.form;
    var laydate = layui.laydate;
    var table = layui.table;
    var $ = layui.jquery;
    var day = new Date();
    var daterange = day.getFullYear() + "-" + (day.getMonth() + 1) + "-" + day.getDate() + " ~ " + day.getFullYear() + "-" + (day.getMonth() + 1) + "-" + day.getDate();
    var logdate = laydate.render({
        elem: '#date',
        range: '~',
        lang: 'en',
        value: daterange,
        done: function (value, date, endDate) {
            daterange = value;
        }
    });
    var tableindex = table.render({
        elem: "#logtable",
        url: '/Log/GetLogs',
        method: 'post',
        where: {
            logtype: $("#logtype").val(),
            daterange: daterange,
            condition: $("#condition").val()
        },
        height: 'full-220',
        cols: [[
            { field: '', type: 'numbers', sort: false },
            { field: 'LogTypeString', title: '日志类型', templet: '#buttonTpl', sort: false },
            { field: 'LoginName', title: '登录名', sort: false },
            { field: 'Ip', title: 'IP地址', sort: false },
            { field: 'CityName', title: '城市地址', sort: false },
            { field: 'LogFunction', title: '日志功能', sort: false },
            { field: 'LogContent', title: '日志内容', sort: false },          
            { field: 'CreationTimeString', title: '创建时间', sort: true },
            { field: '', title: '操作', templet: '#operationTpl' }
        ]],
        done: function (res, curr, count) {
            NProgress.done();
            if (res.code === 1) {
                layer.alert(res.msg);
            }
        },
        page: true
    });
    table.on('tool(logtable)', function (obj) {
        var data = obj.data;
        if (obj.event === 'detail') {
            var temindex = layer.open({
                type: 1,
                area: ['600px', '570px'],
                title: '日志详情',
                content: $('#details').html(),
                success: function (layero, index) {
                    $("#logTypeString").val(data.LogTypeString);
                    $("#loginName").val(data.LoginName);
                    $("#logFunction").val(data.LogFunction);
                    $("#creationTimeString").val(data.CreationTimeString);
                    $("#logContent").val(data.LogContent);
                    $("#cityName").val(data.CityName);
                    $("#ip").val(data.Ip);
                }
            });
        }
    });
    $("#btnSearch").click(function () {
        tableindex.reload({
            url: '/Log/GetLogs',
            method: 'post',
            where: {
                logtype: $("#logtype").val(),
                daterange: daterange,
                condition: $("#condition").val()
            }
        });
    });
});