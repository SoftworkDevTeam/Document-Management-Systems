$(document).ready(function () {
    var $deviceTable = $("#deviceTable");
    var $deviceTableTemplate = $("#deviceTableTemplate");
    var $deviceTbody = $("#deviceTableBody");
    var $barcodeModal = $("#barcodeModal");




    getDevices().then(devices => {
     
        var template = $deviceTableTemplate.html();
        var rendered = Mustache.render(template, { devices: devices });
        $deviceTbody.html(rendered);
        $deviceTable.DataTable({
            ordering: false
        });


    });

    $(document).on("click", ".printBarcode", function () {
        var tr = $(this).closest("tr");
        var sn = tr.data("sn");
        JsBarcode("#barcode", sn, {
            format: "code128",
            lineColor: "#222",
            width: 1,
            fontSize: 12,
            height: 40,
            displayValue: true
        });
        $barcodeModal.modal("show");
    });
    //console.log("working");
});