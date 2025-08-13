function updateCart(shoeDetailId, actionType) {
    fetch('/Admin/BanHangTaiQuay/UpdateCart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            // Thêm token CSRF nếu cần
        },
        body: JSON.stringify({ shoeDetailId, actionType })
    })
    .then(res => res.json())
    .then(data => {
        if (data.success) {
            showToast(data.message, 'success');

            // Cập nhật số lượng giỏ hàng trên UI
            $('#cart-count').text(data.totalItems);

            // Nếu bạn có phần UI khác cần cập nhật cũng làm tương tự
            // Ví dụ: cập nhật tổng tiền, danh sách sản phẩm,...

        } else {
            showToast(data.message || 'Có lỗi xảy ra', 'error');
        }
    })
    .catch(error => {
        showToast('Không thể kết nối tới server', 'error');
        console.error('Error updating cart:', error);
    });
}


export { updateCart };
