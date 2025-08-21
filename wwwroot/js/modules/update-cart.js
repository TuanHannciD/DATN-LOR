export function updateCart(shoeDetailId, actionType, clickedBtn) {
  console.log("updateCart: gửi request", shoeDetailId, actionType);

  if (clickedBtn) clickedBtn.disabled = true;

  fetch("/Admin/BanHangTaiQuay/UpdateCart", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ shoeDetailId, actionType }),
  })
    .then(async (res) => {
      const text = await res.text();
      try {
        return JSON.parse(text);
      } catch {
        throw new Error(`Server response is not JSON: ${text}`);
      }
    })
    .then((data) => {
      console.log("updateCart: response từ server", data);

      if (!data.success) {
        showToast(data.message, "error", "Lỗi");
        return;
      }
      showToast(
        "Cập nhật giỏ hàng thành công. Reload lại sau 2 giây",
        "success"
      );
      // const qty =
      //   typeof data.productQuantity === "number" ? data.productQuantity : 0;
      // window.cartState[shoeDetailId] = qty;

      // // Tạo/ẩn controls theo qty
      // ensureCartControls(shoeDetailId, qty, clickedBtn);

      // // Cập nhật tổng giỏ hàng
      // const totalEl = document.querySelector("#total-cart-items");
      // if (totalEl) {
      //   const total = Object.values(window.cartState).reduce(
      //     (s, q) => s + (q || 0),
      //     0
      //   );
      //   totalEl.textContent = total;
      // }

      // Reload trang sau 2s
      setTimeout(() => location.reload(), 2000);
    })
    .catch((err) => {
      console.error("updateCart: error", err);
      // Reload trang sau 2s nếu cần
      setTimeout(() => location.reload(), 2000);
    })
    .finally(() => {
      if (clickedBtn) clickedBtn.disabled = false;
    });
}
