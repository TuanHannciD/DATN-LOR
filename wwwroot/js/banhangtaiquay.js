$(document).ready(function () {
    // Tìm kiếm sản phẩm
    $('#search-input').on('input', function () {
        var keyword = $(this).val().trim();
        if (keyword.length === 0) {
            $('#search-dropdown').removeClass('show').empty();
            return;
        }
        $.get('/Admin/BanHangTaiQuay/SearchSanPham', { keyword: keyword }, function (data) {
            if (data && data.length > 0) {
                let html = '';
                data.forEach(function (sp) {
                    html += `<button type="button" class="dropdown-item" data-id="${sp.shoeDetailID}">
                        <div><strong>${sp.tenSp}</strong> <span class="text-success ms-2">${sp.gia.toLocaleString()} VNĐ</span></div>
                        <div class="text-muted">Màu: ${sp.mauSac} | Size: ${sp.kichThuoc}</div>
                        <div class="text-muted">Thương hiệu: ${sp.thuongHieu} | Chất liệu: ${sp.chatLieu}</div>
                        <div class="text-muted">Danh mục: ${sp.danhMuc}</div>
            </button>`;
        });
        $('#search-dropdown').html(html).addClass('show');
            } else {
                $('#search-dropdown').html('<div class="dropdown-item text-muted">Không tìm thấy sản phẩm</div>').addClass('show');
            }
        });
    });

    // Khi click vào sản phẩm trong dropdown
    $('#search-dropdown').on('click', '.dropdown-item', function () {
        var shoeDetailID = $(this).data('id');
        $('#hidden-shoeDetailId').val(shoeDetailID);
        $('#add-to-cart-form').submit();
        $('#search-dropdown').removeClass('show').empty();
        $('#search-input').val('');
    });

    // Ẩn dropdown khi click ra ngoài
    $(document).on('click', function (e) {
        if (!$(e.target).closest('#search-input, #search-dropdown').length) {
            $('#search-dropdown').removeClass('show').empty();
        }
    });

    // Tìm kiếm khách hàng
    $('#search-khachhang').on('input', function () {
        var keyword = $(this).val().trim();
        if (keyword.length < 1) {
            $('#dropdown-khachhang').removeClass('show').empty();
            return;
        }
        $.get('/Admin/BanHangTaiQuay/SearchKhachHang', { keyword: keyword }, function (data) {
            if (data && data.length > 0) {
                let html = '';
                data.forEach(function (kh) {
                    html += `<button type="button" class="dropdown-item" data-id="${kh.userID}">
                        <div><strong>${kh.hoTen || kh.tenDangNhap}</strong> <span class="text-muted ms-2">${kh.soDienThoai || ''}</span></div>
                        <div class="text-muted small">${kh.email || ''}</div>
                    </button>`;
                });
                $('#dropdown-khachhang').html(html).addClass('show');
            } else {
                $('#dropdown-khachhang').html('<div class="dropdown-item text-muted">Không tìm thấy khách hàng</div>').addClass('show');
            }
        });
    });

    // Chọn khách hàng
    $('#dropdown-khachhang').on('click', '.dropdown-item', function () {
        var userID = $(this).data('id');
        var ten = $(this).find('strong').text();
        $('#search-khachhang').val(ten);
        $('#selected-khachhang-id').val(userID); // Lưu tạm userID vào hidden input
        $('#dropdown-khachhang').removeClass('show').empty();
        // TODO: Xử lý chọn khách hàng (gán vào hóa đơn, lưu tạm, ...)
    });

    // Ẩn dropdown khi click ra ngoài
    $(document).on('click', function (e) {
        if (!$(e.target).closest('#search-khachhang, #dropdown-khachhang').length) {
            $('#dropdown-khachhang').removeClass('show').empty();
        }
    });

    function formatNumber(n) {
        return n.toLocaleString('vi-VN');
    }
    function tinhThanhTienSauGiam() {
        var tongTien = window.tongTienGoc || 0;
        var giamPhanTram = parseFloat($('#giamgia-phantram').val()) || 0;
        var giamTienMat = parseFloat($('#giamgia-tienmat').val()) || 0;
        var lyDoGiamGia = $('#lydo-giamgia').val().trim();
        var giamGia = 0;
        if (giamPhanTram > 0) giamGia += tongTien * giamPhanTram / 100;
        if (giamTienMat > 0) giamGia += giamTienMat;
        if (lyDoGiamGia.length > 0) giamGia += 10000; // Thêm 10,000 VNĐ cho lý do giảm giá
        if (giamGia > tongTien) giamGia = tongTien;
        var thanhTien = tongTien - giamGia;
        $('#giam-gia-info').text(giamGia > 0 ? `Giảm giá: -${formatNumber(Math.round(giamGia))} VNĐ` : '');
        $('#thanh-tien-sau-giam').text(formatNumber(Math.round(thanhTien)) + ' VNĐ');
    }
    $('#giamgia-phantram, #giamgia-tienmat, #lydo-giamgia').on('input change', tinhThanhTienSauGiam);
    tinhThanhTienSauGiam();

    function tinhThanhTienDong() {
        $(".thanh-tien-dong").each(function () {
            var row = $(this).closest('tr');
            var giaGoc = parseInt(row.data('gia-goc')) || 0;
            var giaSauGiam = parseInt(row.data('gia-sau-giam')) || 0;
            var soLuong = parseInt(row.data('so-luong')) || 1;
            var isTang = row.data('tangkem') == 1;
            var html = '';
            if (isTang) {
                html = '<span class="badge bg-success ms-2" style="font-size: 0.95em;">🎁 Tặng</span> <span class="text-danger fw-bold">0 VNĐ</span>';
            } else if (giaSauGiam < giaGoc) {
                var tongGoc = giaGoc * soLuong;
                var tongSauGiam = giaSauGiam * soLuong;
                html = '<span style="color:#888; font-size:0.95em; text-decoration:line-through;">' + formatNumber(tongGoc) + '</span>';
                html += '<span class="fw-bold text-danger" style="font-size:1em;"> ' + formatNumber(tongSauGiam) + ' VNĐ</span>';
            } else {
                var tong = giaGoc * soLuong;
                html = '<span class="fw-bold text-danger">' + formatNumber(tong) + ' VNĐ</span>';
            }
            $(this).html(html);
        });
    }
    $(document).on('input change', '.chietkhau-phantram, .chietkhau-tienmat, .is-tangkem', function () {
        var row = $(this).closest('tr');
        var cartDetailId = row.find('.chietkhau-phantram').data('id');
        var ckpt = parseFloat(row.find('.chietkhau-phantram').val()) || 0;
        var cktm = parseFloat(row.find('.chietkhau-tienmat').val()) || 0;
        var isTang = row.find('.is-tangkem').is(':checked');
        $.post('/Admin/BanHangTaiQuay/UpdateDiscountCartItem', {
            cartDetailId: cartDetailId,
            chietKhauPhanTram: ckpt,
            chietKhauTienMat: cktm,
            isTangKem: isTang
        }, function () {
            location.reload();
        });
    });
    tinhThanhTienDong();

    function tinhThanhTienDongHoaDon() {
        $(".thanh-tien-dong-hoa-don").each(function () {
            var row = $(this).closest('tr');
            var giaGoc = parseInt(row.data('gia-goc')) || 0;
            var giaSauGiam = parseInt(row.data('gia-sau-giam')) || 0;
            var soLuong = parseInt(row.data('so-luong')) || 1;
            var isTang = row.data('tangkem') == 1;
            var html = '';
            if (isTang) {
                html = '<span class="badge bg-success ms-2" style="font-size: 0.95em;">🎁 Tặng</span> <span class="text-danger fw-bold">0 VNĐ</span>';
            } else if (giaSauGiam < giaGoc) {
                var tongGoc = giaGoc * soLuong;
                var tongSauGiam = giaSauGiam * soLuong;
                html = '<span style="color:#888; font-size:0.95em; text-decoration:line-through;">' + formatNumber(tongGoc) + '</span>';
                html += '<span class="fw-bold text-danger" style="font-size:1em;"> ' + formatNumber(tongSauGiam) + ' VNĐ</span>';
            } else {
                var tong = giaGoc * soLuong;
                html = '<span class="fw-bold text-danger">' + formatNumber(tong) + ' VNĐ</span>';
            }
            $(this).html(html);
        });
    }

    tinhThanhTienDongHoaDon();

    let currentCartDetailId = null;
    let discountType = 'percent';

    // Mở modal khi click nút 🎁 % hoặc chuột phải dòng
    $(document).on('click', '.bhq-btn-discount', function (e) {
        e.stopPropagation();
        currentCartDetailId = $(this).data('cartid');
        openDiscountModal();
    });
    $(document).on('contextmenu', 'tr:has(.bhq-cart-action)', function (e) {
        e.preventDefault();
        currentCartDetailId = $(this).find('.bhq-btn-discount').data('cartid');
        openDiscountModal();
    });
    function openDiscountModal() {
        var modal = new bootstrap.Modal(document.getElementById('discountModal'));
        $('#discount-value').val(0);
        $('#discount-reason').val('');
        $('#discount-tangkem').prop('checked', false);
        setDiscountType('percent');
        setTimeout(() => { $('#discount-value').focus(); }, 300);
        modal.show();
    }
    // Toggle %/VNĐ
    $(document).on('click', '.btn-toggle-type', function () {
        const type = $(this).data('type');
        setDiscountType(type);
    });
    function setDiscountType(type) {
        discountType = type;
        if (type === 'percent') {
            $('.btn-toggle-type[data-type="percent"]').addClass('btn-primary').removeClass('btn-outline-primary');
            $('.btn-toggle-type[data-type="amount"]').addClass('btn-outline-primary').removeClass('btn-primary');
            $('#discount-value').attr('max', 100).attr('step', '0.01').attr('placeholder', '0');
            $('.btn-quick-value').show();
        } else {
            $('.btn-toggle-type[data-type="amount"]').addClass('btn-primary').removeClass('btn-outline-primary');
            $('.btn-toggle-type[data-type="percent"]').addClass('btn-outline-primary').removeClass('btn-primary');
            $('#discount-value').removeAttr('max').attr('step', '1000').attr('placeholder', '0');
            $('.btn-quick-value').hide();
        }
    }
    // Đề xuất nhanh
    $(document).on('click', '.btn-quick-value', function () {
        $('#discount-value').val($(this).data('value'));
    });
    // Lưu chiết khấu/tặng
    $(document).on('click', '#btn-save-discount', function () {
        const value = parseFloat($('#discount-value').val()) || 0;
        const reason = $('#discount-reason').val().trim();
        const isTang = $('#discount-tangkem').is(':checked');
        if (!reason) {
            $('#discount-reason').addClass('is-invalid');
            $('#discount-reason').focus();
            return;
        } else {
            $('#discount-reason').removeClass('is-invalid');
        }
        let chietKhauPhanTram = null, chietKhauTienMat = null;
        if (discountType === 'percent') chietKhauPhanTram = value;
        else chietKhauTienMat = value;
        $.ajax({
            url: '/Admin/BanHangTaiQuay/UpdateDiscountCartItem',
            type: 'POST',
            data: {
                cartDetailId: currentCartDetailId,
                chietKhauPhanTram: chietKhauPhanTram,
                chietKhauTienMat: chietKhauTienMat,
                isTangKem: isTang,
                lyDoGiamGia: reason
            },
            success: function () {
                var modal = bootstrap.Modal.getInstance(document.getElementById('discountModal'));
                modal.hide();
                location.reload();
            },
            error: function () {
                alert('Có lỗi khi cập nhật chiết khấu/tặng!');
            }
        });
    });
    // Đóng modal reset form
    $('#discountModal').on('hidden.bs.modal', function () {
        $('#discount-value').val(0);
        $('#discount-reason').val('');
        $('#discount-tangkem').prop('checked', false);
        $('#discount-reason').removeClass('is-invalid');
    });

    function tinhDonGiaDong() {
        $(".bhq-cart-price").each(function () {
            var row = $(this).closest('tr');
            var giaGoc = parseInt(row.data('gia-goc')) || 0;
            var giaSauGiam = parseInt(row.data('gia-sau-giam')) || 0;
            var isTang = row.data('tangkem') == 1;
            var html = '';
            if (isTang) {
                html = '<span class="badge bg-success ms-2" style="font-size: 0.95em;">🎁 Tặng</span>';
            } else if (giaSauGiam < giaGoc) {
                html = '<span style="color:#888; font-size:0.95em; text-decoration:line-through;">' + formatNumber(giaGoc) + '</span>';
                html += '<span class="fw-bold text-danger" style="font-size:1em;"> ' + formatNumber(giaSauGiam) + '</span>';
            } else {
                html = '<span class="fw-bold">' + formatNumber(giaGoc) + '</span>';
            }
            $(this).html(html);
        });
    }

    tinhDonGiaDong();

    function tinhDonGiaDongHoaDon() {
        $(".bhq-cart-price-hoa-don").each(function () {
            var row = $(this).closest('tr');
            var giaGoc = parseInt(row.data('gia-goc')) || 0;
            var giaSauGiam = parseInt(row.data('gia-sau-giam')) || 0;
            var isTang = row.data('tangkem') == 1;
            var html = '';
            if (isTang) {
                html = '<span class="badge bg-success ms-2" style="font-size: 0.95em;">🎁 Tặng</span>';
            } else if (giaSauGiam < giaGoc) {
                html = '<span style="color:#888; font-size:0.95em; text-decoration:line-through;">' + formatNumber(giaGoc) + '</span>';
                html += '<span class="fw-bold text-danger" style="font-size:1em;"> ' + formatNumber(giaSauGiam) + '</span>';
            } else {
                html = '<span class="fw-bold">' + formatNumber(giaGoc) + '</span>';
            }
            $(this).html(html);
        });
    }

    tinhDonGiaDongHoaDon();
}); 