var config = {
    pageIndex: 1,
    pageSize: 10
};

(function (window, $) {
    window.employee = {
        init: function () {
            employee.loadEmployees();
        },
        regisControl: function () {
            $('.cbStatus').off('click').on('click', function (e) {
                var Id = $(this).data("id");
                $.ajax({
                    url: _mainConfig.relativePath + '/Employee/ChangeStatus',
                    data: { Id: Id },
                    type: 'post',
                    success: function (res) {
                        if (res.status) {
                            toastr["success"]("Cập nhật thành công.");
                        }
                        else {
                            toastr["error"]("Có lỗi xảy ra, vui lòng thử lại.");
                            setTimeout(function(){ location.reload() }, 2500);
                        }
                    }
                });
            });
            $('.employee_delete').off('click').on('click',function(e){
                e.preventDefault();
                var Id = $(this).data("id");
                $.ajax({
                    url: _mainConfig.relativePath + '/Employee/Delete',
                    data: { Id: Id },
                    type: 'post',
                    success: function (res) {
                        if (res.status) {
                            toastr["success"]("Cập nhật thành công.");
                            setTimeout(function(){ location.reload() }, 2500);
                        }
                        else {
                            toastr["error"]("Không thể xóa, danh mục có chứa sản phẩm.");
                        }
                    }
                });
            });
        },
        paging: function (totalRow, callBack, changePageSize) {
            var totalPage = Math.ceil(totalRow / config.pageSize);

            $('#paginationUL').twbsPagination({
                totalPages: totalPage,
                visiblePages: 5,
                first: '<<',
                last: '>>',
                prev: '',
                next: '',
                onPageClick: function (event, p) {
                    config.pageIndex = p;
                    callBack();
                }
            });
        },
        loadEmployees: function (isPageChanged) {
            $.ajax({
                url: _mainConfig.relativePath + '/Employee/ListAll_Paging',
                type: 'get',
                contentType: 'application/json',
                data: { pageIndex: config.pageIndex, pageSize: config.pageSize },
                success: function (res) {
                    if (res.status) {
                        $("#tbody").html('');
                        $("#tbody").html(res.data);
                        employee.paging(res.totalRow, function () { employee.loadEmployees(); }, isPageChanged);
                        employee.regisControl();
                    }
                }
            })
        }
    }
})(window, jQuery);

$(function () {
    employee.init();
});
