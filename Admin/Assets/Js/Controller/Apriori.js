(function (window, $) {
    window.apriori = {
        rules: [],
        init: function () {
            const me = this;
            me.resetForm();
            me.registerEvent();
            me.initTransactionTable();
            me.initSupportTable();
        },
        resetForm: function () {
            const me = this;

            me.activeTab('#tabTransaction');
        },
        registerEvent: function () {
            const me = this;

            $('#btnExecuteApriori').off('click').on('click', function (e) {
                e.preventDefault();
                me.executeApriori();
            });

            $('#btnSaveResult').off('click').on('click', function (e) {
                e.preventDefault();
                me.saveAprioriResult();
            });
        },
        activeTab: function (tabId) {
            $('#rootwizard [data-toggle="tab"]').removeClass("active");
            $('#rootwizard [href="' + tabId + '"]').addClass("active");
        },
        initTransactionTable: function () {
            $(document).ready(function () {
                $('#tblTransactions').DataTable({
                    ajax: '/Apriori/GetOrders',
                    autoWidth: false,
                    columns: [
                        { data: 'Id' },
                        {
                            data: 'ProductIds',
                            render: function (data, type, row) {
                                if (data) {
                                    data = data.slice(1, data.length - 1).split(",").join("<br/>");
                                }
                                return data;
                            }
                        },
                        {
                            data: 'ProductNames',
                            render: function (data, type, row) {
                                return data.split("|").join("<br/>");
                            }
                        }
                    ],
                    columnDefs: [
                        { width: '60%', targets: 2 }
                    ]
                });
            });
        },
        initSupportTable: function () {
            $(document).ready(function () {
                $('#tblSupports').DataTable({
                    ajax: '/Apriori/GetSupports',
                    autoWidth: false,
                    columns: [
                        {
                            data: 'Id',
                            render: function (data, type, row) {
                                if (data) {
                                    data = data.slice(1, data.length - 1);
                                }
                                return data;
                            }
                        },
                        { data: 'Name' },
                        { data: 'Support' }
                    ],
                    columnDefs: [
                        { width: '60%', targets: 1 }
                    ],
                    order: [[2, "desc"]]
                });
            });
        },
        executeApriori: function () {
            const me = this;

            $('#tabExecApriori').html('');
            $('#frmMinSupport #validate-summary').addClass('hide');

            if ($('#txtMinSupport').val()) {
                $.ajax({
                    url: _mainConfig.relativePath + '/Apriori/Execute',
                    data: {
                        minSupport: $('#txtMinSupport').val(),
                        confidence: 0
                    },
                    type: 'post',
                    success: function (res) {
                        if (res.AllProcess) {
                            var allProcess = res.AllProcess;
                            var level = 1;
                            var next = true;
                            do {
                                next = false;

                                var cLevel = 'C' + level;
                                if (allProcess.hasOwnProperty(cLevel)) {
                                    next = true;
                                    me.generateCTable(cLevel, allProcess[cLevel]);
                                }

                                var lLevel = 'L' + level;
                                if (allProcess.hasOwnProperty(lLevel)) {
                                    next = true;
                                    me.generateLTable(lLevel, allProcess[lLevel]);
                                }

                                level += 1;
                            } while (next);
                        }

                        if (res.StrongRules && res.StrongRules.length > 0) {
                            me.generateStrongRules(res.StrongRules);
                            me.rules = res.StrongRules;
                        }

                        toastr["success"]("Thực thi thuật toán thành công.");
                    }
                })
            }
            else {
                $('#frmMinSupport #validate-summary').html('Yêu cầu nhập minimum support!');
                $('#frmMinSupport #validate-summary').removeClass('hide');
            }
        },

        generateCTable(name, data) {
            if (data && Object.keys(data).length === 0 && Object.getPrototypeOf(data) === Object.prototype) return;

            var body = '', table = '';
            var tableName = 'tbl' + name;

            for (var propertyName in data) {
                body += `<tr>
                            <td>`+ propertyName.slice(1, propertyName.length - 1) + `</th>
                        </tr>`;
            }

            table += `<div><h2>` + name +`</h2>
                            <table id="`+ tableName + `" class="display padding-30" style="width:100%">
                                <thead>
                                    <tr>
                                        <th>ItemSet</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    `+ body + `
                                </tbody>
                            </table>
                        </div>`;

            $('#tabExecApriori').append(table);
            $('#' + tableName).DataTable({
                "searching": false,
                "lengthChange": false
            });
        },
        generateLTable(name, data) {
            if (data.length == 0)
                return;

            var body = '', table = '';
            var tableName = 'tbl' + name;

            $.each(data, function (i, item) {
                body += `<tr>
                            <td>`+ item.Name.slice(1, item.Name.length - 1) +`</th>
                            <td>`+ item.Support +`</th>
                        </tr>`;
            });

            table += `<div class=""><h2>` + name +`</h2>
                            <table id="`+ tableName +`" class="display padding-30" style="width:100%">
                                <thead>
                                    <tr>
                                        <th>ItemSet</th>
                                        <th>Support</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    `+ body +`
                                </tbody>
                            </table>
                        </div>`;

            $('#tabExecApriori').append(table);
            $('#' + tableName).DataTable({
                "searching": false,
                "lengthChange": false
            });
        },
        generateStrongRules: function (data) {
            var body = '', table = '';

            $.each(data, function (i, item) {
                body += `<tr>
                            <td>`+ item.X.join(',') + ` => `+ item.Y.join(',') +`</th>
                            <td>`+ Math.round(item.Confidence * 100) + `%</th>
                        </tr>`;
            });

            table += `<div class="">
                            <table id="tblStrongRules" class="display padding-30" style="width:100%">
                                <thead>
                                    <tr>
                                        <th>Luật</th>
                                        <th>Độ tin cậy</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    `+ body + `
                                </tbody>
                            </table>
                        </div>`;

            $('.apriori-result').html(table);
            $('#tblStrongRules').DataTable({
                "searching": false,
                "lengthChange": false
            });
        },
        saveAprioriResult: function () {
            const me = this;
            debugger
            $('#frmConfidence #validate-summary').addClass('hide');
            var minConfidence = $('#txtConfidence').val();
            if (minConfidence >= 0) {

                var rules = [];
                $.each(me.rules, function (i, item) {
                    if (item.Confidence >= minConfidence) {
                        rules.push({
                            X: item.X.join(','),
                            Y: item.Y.join(','),
                            Confidence: item.Confidence
                        });
                    }
                });

                $.ajax({
                    url: _mainConfig.relativePath + '/Apriori/SaveResult',
                    type: 'post',
                    data: JSON.stringify({ 'rules': rules }),
                    dataType: 'json',
                    contentType: 'application/json',
                    success: function (res) {
                        if (res.Success) {
                            toastr["success"]("Áp dụng thành công.");
                        }
                    }
                })
            }
            else {
                $('#frmConfidence #validate-summary').html('Yêu cầu nhập confidence!');
                $('#frmConfidence #validate-summary').removeClass('hide');
            }
        }
    }
})(window, jQuery);

$(function () {
    apriori.init();
});
