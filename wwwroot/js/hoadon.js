document.addEventListener("DOMContentLoaded", function () {
  // Hiển thị xem hóa đơn
  const modalEl = document.getElementById("isOpenModal");
  const modalObj = new bootstrap.Modal(modalEl);

  // Reload trang sau khi modal đóng (nếu muốn thì bật)
  // modalEl.addEventListener("hidden.bs.modal", function () {
  //     location.reload();
  // });

  // Bắt sự kiện click cho tất cả nút Xem
  const thoiGianSelect = document.getElementById("thoiGianSelect");
  const startDate = document.getElementById("startDate");
  const endDate = document.getElementById("endDate");
  const trangThaiSelect = document.getElementById("trangThaiSelect");
  const hinhThucSelect = document.getElementById("hinhThucSelect");
  const applyFilter = document.getElementById("applyFilter");
  const phoneFilter = document.getElementById("phoneFilter");
  const tbody = document.getElementById("hoaDonTableBody");
  const idFilter = document.getElementById("idFilter");
  const nameFilter = document.getElementById("nameFilter");
  const trangThaiTT = document.getElementById("trangThaiTT");
  const nameCreateFilter = document.getElementById("nameCreateFilter");

  console.log("phoneFilterphoneFilter:", phoneFilter.value);

  function formatDate(date) {
    return date.toISOString().split("T")[0];
  }

  function updateDateRange(value) {
    const today = new Date();
    let start, end;

    if (value === "today") {
      start = end = today;
    } else if (value === "month") {
      start = new Date(today.getFullYear(), today.getMonth(), 1);
      end = new Date(today.getFullYear(), today.getMonth() + 1, 0);
    } else if (value === "year") {
      start = new Date(today.getFullYear(), 0, 1);
      end = new Date(today.getFullYear(), 11, 31);
    } else {
      return;
    }

    startDate.value = formatDate(start);
    endDate.value = formatDate(end);
  }

  updateDateRange(thoiGianSelect.value);

  thoiGianSelect.addEventListener("change", function () {
    updateDateRange(this.value);
  });

  function loadHoaDon() {
    const params = new URLSearchParams({
      startDate: startDate.value,
      endDate: endDate.value,
      trangThai: trangThaiSelect.value,
      hinhThuc: hinhThucSelect.value,
      phone: phoneFilter.value,
      idFilter: idFilter.value,
      nameFilter: nameFilter.value,
      trangThaiTT: trangThaiTT.value,
      nameCreateFilter: nameCreateFilter.value,
    });

    fetch(`/Admin/HoaDon/GetHoaDons?${params.toString()}`)
      .then((res) => res.json())
      .then((data) => {
        tbody.innerHTML = "";
        data.forEach((item) => {
          const tr = document.createElement("tr");
          tr.innerHTML = `
          <td>${item.hoaDonID.substring(0, 8)}...</td>
          <td>${item.tenKhachHang}</td>
          <td>${item.soDienThoai}</td>
          <td>
            <span class="badge ${getBadgeClassFromText(
              item.trangThaiDisplay
            )} trangThaiCell"
                  data-id="${item.hoaDonID}">
              ${item.trangThaiDisplay}
            </span>
          </td>
          <td>${item.hinhThucThanhToanDisplay}</td>
          <td>${item.tongTien.toLocaleString()}</td>
          <td>${item.daThanhToan ? "Đã thanh toán" : "Chưa thanh toán"}</td>
          <td>${item.nguoiTao}</td>
          <td>${
            item.ngayTao ? new Date(item.ngayTao).toLocaleString() : ""
          }</td>
          <td class="text-center">
            <a class="btn btn-info btn-sm me-1" href="#" data-id="${
              item.hoaDonID
            }">
              <i class="mdi mdi-eye"></i> Xem
            </a>
          </td>
        `;
          tbody.appendChild(tr);
        });
      });
  }

  // Bắt sự kiện click badge trạng thái
  tbody.addEventListener("click", function (e) {
    // --- XEM HÓA ĐƠN ---
    const xemBtn = e.target.closest(".btn-info"); // Nút xem
    if (xemBtn) {
      const hoaDonID = xemBtn.dataset.id;
      console.log("Xem hóa đơn ID:", hoaDonID);

      // Tạm hiển thị modal
      const modalContent = document.getElementById("modalContent");
      modalContent.innerHTML = `<p>Đang load chi tiết hóa đơn: <strong>${hoaDonID}</strong></p>`;
      const modalEl = document.getElementById("isOpenModal");
      const modalObj = new bootstrap.Modal(modalEl);
      modalObj.show();
      return; // Dừng sự kiện ở đây
    }

    const target = e.target.closest(".trangThaiCell");
    if (!target) return;

    const hoaDonID = target.dataset.id;
    console.log("hoaDonID:", hoaDonID);

    if (!confirm("Bạn có chắc chắn muốn thay đổi trạng thái đơn hàng này?"))
      return;

    fetch(`/Admin/HoaDon/UpdateTrangThai`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ HoaDonID: hoaDonID }),
    })
      .then((res) => res.json())
      .then((data) => {
        if (data.success) {
          showToast(data.message.message, data.success);
          console.log("data:", data);

          loadHoaDon(); // Load lại bảng để thấy trạng thái mới
        } else {
          showToast(data.message, data.success);
          console.log("data:", data);
        }
      })
      .catch((err) => console.error(err));
  });

  applyFilter.addEventListener("click", loadHoaDon);

  // Load mặc định hôm nay
  loadHoaDon();
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
