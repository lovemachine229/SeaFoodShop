(function (window, $) {
    window.aprioriResult = {
        rules: [],
        init: function () {
            const me = this;
            me.initResultTable();
        },
        initResultTable: function () {
            $(document).ready(function () {
                $('#tblAprioriResult').DataTable({
                    ajax: '/Apriori/GetRules',
                    autoWidth: false,
                    columns: [
                        {
                            data: 'X',
                            render: function (data, type, row) {
                                return row.X + ` => ` + row.Y;
                            }
                        },
                        {
                            data: 'InputName',
                            render: function (data, type, row) {
                                var inputDes = data.split("|").join("<br/>");
                                return inputDes;
                            }
                        },
                        {
                            data: 'OutputName',
                            render: function (data, type, row) {
                                var ouputDes = data.split("|").join("<br/>");
                                return ouputDes;
                            }
                        },
                        {
                            data: 'Confidence',
                            render: function (data, type, row) {
                                return Math.round(data * 100) + '%';
                            }
                        }
                    ],
                    columnDefs: [
                        { width: '30%', targets: 1 },
                        { width: '30%', targets: 2 },
                        { width: '10%', targets: 3 }
                    ],
                    order: [[3, "desc"]]
                });
            });
        },
    }
})(window, jQuery);

$(function () {
    aprioriResult.init();
});
