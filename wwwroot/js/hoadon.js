document.addEventListener("DOMContentLoaded", function () {
  // Gán màu khi reload dựa trên text
  document.querySelectorAll(".status-badge").forEach(function (badge) {
    badge.className = "badge status-badge"; // reset class cũ
    badge.classList.add(getBadgeClassFromText(badge.textContent));

    badge.addEventListener("click", async function () {
      const hoaDonID = this.getAttribute("data-id");
      const badgeEl = this;
      try {
        if (!confirm("Bạn có chắc muốn cập nhật trạng thái hóa đơn?")) return;
        const res = await fetch(
          `/Admin/HoaDon/UpdateTrangThai?HoaDonID=${hoaDonID}`,
          { method: "POST" }
        );
        const data = await res.json();

        if (data.success) {
          badgeEl.textContent = data.message.data.newStatusDisplay;
          badgeEl.className = "badge status-badge";
          badgeEl.classList.add(
            getBadgeClassFromText(data.message.data.newStatusDisplay)
          );
        } else {
          showToast(data.message, "error");
        }
      } catch (err) {
        console.error("Lỗi khi cập nhật trạng thái:", err);
        showToast("Có lỗi kết nối server!");
      }
    });
  });
  // Gán sự kiện click cho nút HuyHoaDon
  document.querySelectorAll(".btn-huy-hoa-don").forEach(function (btn) {
    btn.addEventListener("click", async function (e) {
      e.preventDefault(); // ngăn href #
      const hoaDonID = this.getAttribute("data-id");

      if (!confirm("Bạn có chắc muốn xóa hoặc gỡ xóa?")) return;

      try {
        const res = await fetch(`/Admin/HoaDon/HuyHoaDon?id=${hoaDonID}`, {
          method: "POST",
        });
        const data = await res.json();
        console.log("data:", data);

        if (res.ok) {
          // Cập nhật badge tương ứng trên cùng 1 row (nếu có)
          const badgeEl = document.querySelector(
            `.status-badge[data-id="${hoaDonID}"]`
          );
          if (badgeEl) {
            badgeEl.textContent = data.data.newStatus;

            badgeEl.classList.add(getBadgeClassFromText(data.data.newStatus));
          }
          showToast(data.message, "success");
        } else {
          showToast(data.message, "error");
        }
      } catch (err) {
        console.error("Lỗi khi hủy hóa đơn:", err);
        showToast("Có lỗi kết nối server!");
      }
    });
  });
});

function getBadgeClassFromText(text) {
  switch (text.trim()) {
    case "Chờ xác nhận":
      return "bg-dark";
    case "Đã xác nhận":
      return "bg-primary";
    case "Đang giao hàng":
      return "bg-warning";
    case "Đã giao":
      return "bg-success";
    case "Đã hủy":
      return "bg-danger";
    case "Đã thanh toán":
      return "bg-primary";
    default:
      return "bg-light";
  }
}
