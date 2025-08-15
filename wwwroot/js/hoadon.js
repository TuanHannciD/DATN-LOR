document.addEventListener("DOMContentLoaded", function () {
  const $select = $("#selectStatus"); // Select2 dùng jQuery

  document.querySelectorAll(".status-badge").forEach(function (badge) {
    badge.addEventListener("click", function () {
      const hoaDonId = this.getAttribute("data-id");

      // Lưu ID vào modal
      document.getElementById("hdHoaDonId").value = hoaDonId;

      // Lấy danh sách trạng thái từ server
      fetch(`/Admin/HoaDon/GetTrangThaiList`)
        .then((res) => res.json())
        .then((data) => {
          $select.empty(); // Xóa option cũ
          $select.append(
            "<option disabled selected>Chọn trạng thái...</option>"
          );
          data.forEach((s) => {
            $select.append(`<option value="${s.value}">${s.text}</option>`);
          });

          // Khởi tạo Select2 (hoặc refresh nếu đã khởi tạo trước)
          if ($select.hasClass("select2-hidden-accessible")) {
            $select.select2("destroy"); // xóa instance cũ
          }

          $select.select2({
            theme: "bootstrap-5",
            dropdownParent: $("#updateStatusModal"),
            width: "100%",
            placeholder: "Chọn trạng thái...",
          });
        });

      // Hiển thị modal
      const modal = new bootstrap.Modal(
        document.getElementById("updateStatusModal")
      );
      modal.show();
    });
  });

  // Submit form
  document
    .getElementById("updateStatusForm")
    .addEventListener("submit", function (e) {
      e.preventDefault();
      const formData = new FormData(this);
      fetch("/Admin/HoaDon/UpdateTrangThai", {
        method: "POST",
        body: formData,
      })
        .then((res) => res.json())
        .then((data) => {
          if (data.success) {
            showToast(data.message, "success");
            setTimeout(() => {
              location.reload(); // reload sau 1 giây
            }, 1000);
          } else {
            showToast(data.message, "error");
          }
        });
    });
});
