var config = {
    pageIndex: 1,
    pageSize: 10
};

(function (window, $) {
    window.role = {
        init: function () {
            role.loadRoles();
        },
        regisControl: function () {
            $('.cbStatus').off('click').on('click', function (e) {
                var Id = $(this).data("id");
                $.ajax({
                    url: _mainConfig.relativePath + '/Role/ChangeStatus',
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
            $('.role_delete').off('click').on('click',function(e){
                e.preventDefault();
                var Id = $(this).data("id");
                $.ajax({
                    url: _mainConfig.relativePath + '/Role/Delete',
                    data: { Id: Id },
                    type: 'post',
                    success: function (res) {
                        if (res.status) {
                            toastr["success"]("Cập nhật thành công.");
                            setTimeout(function(){ location.reload() }, 2500);
                        }
                        else {
                            toastr["error"]("Không thể xóa vai trò đang được sử dụng.");
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
        loadRoles: function (isPageChanged) {
            $.ajax({
                url: _mainConfig.relativePath + '/Role/ListAll_Paging',
                type: 'get',
                contentType: 'application/json',
                data: { pageIndex: config.pageIndex, pageSize: config.pageSize },
                success: function (res) {
                    if (res.status) {
                        $("#tbody").html('');
                        $("#tbody").html(res.data);
                        role.paging(res.totalRow, function () { role.loadRoles(); }, isPageChanged);
                        role.regisControl();
                    }
                }
            })
        }
    }
})(window, jQuery);

$(function () {
    role.init();
});
