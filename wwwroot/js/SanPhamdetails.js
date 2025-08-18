document.addEventListener("DOMContentLoaded", () => {
  const colorBtns = document.querySelectorAll(".mau-option");
  const sizeBtns = document.querySelectorAll(".size-option");
  const colorInp = document.getElementById("selectedColorId");
  const sizeInp = document.getElementById("selectedSizeId");
  const productId = document.getElementById("productId").value;
  const stockEl = document.getElementById("stockStatus");
  const giaEl = document.getElementById("giaSanPham");

  const toastBox = document.getElementById("toastBox");
  const toastTxt = document.getElementById("toastText");

  window.hideToast = () => toastBox.classList.remove("show");

  function showToast(msg, isSuccess = true) {
    toastTxt.textContent = msg;
    toastBox.style.background = isSuccess ? "#28a745" : "#dc3545";
    toastBox.classList.add("show");
    setTimeout(hideToast, 3000);
  }

  function updateStockAndPrice() {
    if (!colorInp.value || !sizeInp.value) return;

    // Lấy số lượng tồn
    fetch(
      `/Home/GetSoLuongTon?shoeId=${productId}&colorId=${colorInp.value}&sizeId=${sizeInp.value}`
    )
      .then((res) => res.json())
      .then((data) => {
        if (data.success) {
          stockEl.textContent = ` Kho: ${data.soLuong} sản phẩm`;
          stockEl.style.color = "#4caf50";
        } else {
          stockEl.textContent = "Không có hàng";
          stockEl.style.color = "#dc3545";
        }
      });

    // Lấy giá
    fetch(
      `/Home/GetGiaChiTiet?shoeId=${productId}&colorId=${colorInp.value}&sizeId=${sizeInp.value}`
    )
      .then((res) => res.json())
      .then((data) => {
        if (data.success) {
          giaEl.textContent = data.gia.toLocaleString("vi-VN") + " VNĐ";
        } else {
          giaEl.textContent = "Không có giá";
        }
      });
  }

  // Chọn màu
  colorBtns.forEach((btn) =>
    btn.addEventListener("click", () => {
      colorBtns.forEach((b) => b.classList.remove("selected"));
      btn.classList.add("selected");
      colorInp.value = btn.dataset.id;
      updateStockAndPrice();
    })
  );

  // Chọn size
  sizeBtns.forEach((btn) =>
    btn.addEventListener("click", () => {
      sizeBtns.forEach((b) => b.classList.remove("selected"));
      btn.classList.add("selected");
      sizeInp.value = btn.dataset.id;
      updateStockAndPrice();
    })
  );

  // Tăng/giảm số lượng
  document.getElementById("decreaseQty").addEventListener("click", () => {
    const qty = document.getElementById("quantity");
    if (parseInt(qty.value) > 1) qty.value = parseInt(qty.value) - 1;
  });

  document.getElementById("increaseQty").addEventListener("click", () => {
    const qty = document.getElementById("quantity");
    qty.value = parseInt(qty.value) + 1;
  });

  // AJAX thêm vào giỏ hàng
  document.getElementById("addToCartForm").addEventListener("submit", (e) => {
    e.preventDefault();
    if (!colorInp.value || !sizeInp.value) {
      showToast("Vui lòng chọn đầy đủ màu và kích cỡ!", false);
      return;
    }

    const data = {
      id: productId,
      MauSacId: colorInp.value,
      KichCoId: sizeInp.value,
      quantity: document.getElementById("quantity").value,
      __RequestVerificationToken: document.querySelector(
        'input[name="__RequestVerificationToken"]'
      ).value,
    };

    fetch("/Home/Cart", {
      method: "POST",
      headers: { "Content-Type": "application/x-www-form-urlencoded" },
      body: new URLSearchParams(data),
    })
      .then((r) => r.json())
      .then((res) => {
        if (res.redirect) {
          window.location.href = res.redirect;
          return;
        }
        showToast(res.message, res.success);
      });
  });
});
