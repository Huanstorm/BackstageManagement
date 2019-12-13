layui.use(['form', 'laydate', 'table', 'jquery'], function () {
    NProgress.start();
    var form = layui.form;
    var laydate = layui.laydate;
    var table = layui.table;
    var $ = layui.jquery;
    
    //var tableindex = table.render({
    //    elem: "#workbenchtable",
    //    url: '/Home/GetWorkStationInfo',
    //    method: 'post',
    //    where: {
    //    },
    //    height: 'full-170',
    //    //cols: [[
    //    //    { field: '', type: 'numbers', title: 'Number', sort: false, width: 100 },
    //    //    { field: 'WorkStationNo', title: 'WorkStationNo', sort: false },
    //    //    { field: 'UNTMaterialNo', title: 'UNTMaterialNo', sort: false },
    //    //    { field: 'QualifiedNum', title: 'QualifiedNum', sort: false },
    //    //    { field: 'NoQualifiedNum', title: 'NoQualifiedNum', sort: false },
    //    //    { field: 'WorkStationStatusString', title: 'WorkStationStatus', sort: false },
    //    //]],
    //    done: function (res, curr, count) {
    //        if (res.code == "1") {
    //            layer.open({
    //                title: 'Information',
    //                content: "Search Failed，" + res.msg,
    //                btn: ['OK']
    //            });
    //        }
    //    },
    //    //page: true
    //});
    window.loadtable = function () {
        table.reload("workbenchtable");
        //alert(21321);
        console.log("加载table");
    }
    NProgress.done();
    //window.setInterval(loadtable, 5000);
    
})
