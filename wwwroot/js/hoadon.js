document.addEventListener("DOMContentLoaded", function () {
  // Hiển thị modal
  const modalEl = document.getElementById("isOpenModal");
  const modalObj = new bootstrap.Modal(modalEl);

  // Lấy các element
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

  // Format ngày + giờ
  function formatDate(date) {
    const yyyy = date.getFullYear();
    const mm = String(date.getMonth() + 1).padStart(2, "0");
    const dd = String(date.getDate()).padStart(2, "0");
    const hh = String(date.getHours()).padStart(2, "0");
    const min = String(date.getMinutes()).padStart(2, "0");
    const ss = String(date.getSeconds()).padStart(2, "0");
    return `${yyyy}-${mm}-${dd} ${hh}:${min}:${ss}`;
  }

  function updateDateRange(value) {
    const now = new Date();
    let start, end;

    if (value === "today") {
      start = new Date(
        now.getFullYear(),
        now.getMonth(),
        now.getDate(),
        0,
        0,
        0,
        0
      ); // giờ hiện tại
      end = new Date(
        now.getFullYear(),
        now.getMonth(),
        now.getDate(),
        23,
        59,
        59,
        999
      );
    } else if (value === "month") {
      start = new Date(now.getFullYear(), now.getMonth(), 1, 0, 0, 0, 0);
      end = new Date(now.getFullYear(), now.getMonth() + 1, 0, 23, 59, 59, 999);
    } else if (value === "year") {
      start = new Date(now.getFullYear(), 0, 1, 0, 0, 0, 0);
      end = new Date(now.getFullYear(), 11, 31, 23, 59, 59, 999);
    } else return;

    startDate.value = formatDate(start);
    endDate.value = formatDate(end);
    console.log("startDate:", startDate.value, "endDate:", endDate.value);
  }

  // Gọi khi load
  updateDateRange(thoiGianSelect.value);

  // Thay đổi select
  thoiGianSelect.addEventListener("change", function () {
    updateDateRange(this.value);
  });
  // thoiGianSelect.addEventListener("change", function () {
  //   updateDateRange(this.value);
  // });

  // Load danh sách hóa đơn
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
    console.log("Dữ liệu gửi đi:", {
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
        console.log("Dữ liệu nhận về:", data);
        tbody.innerHTML = "";
        data.forEach((item) => {
          const tr = document.createElement("tr");
          tr.innerHTML = `
            <td>${item.hoaDonID?.substring(0, 8) ?? ""}...</td>
            <td>${item.tenKhachHang ?? ""}</td>
            <td>${item.soDienThoai ?? ""}</td>
            <td>
              <span class="badge ${getBadgeClassFromText(
                item.trangThaiDisplay
              )} trangThaiCell"
                    data-id="${item.hoaDonID ?? ""}">
                ${item.trangThaiDisplay ?? ""}
              </span>
            </td>
            <td>${item.hinhThucThanhToanDisplay ?? ""}</td>
            <td>${
              item.tongTien != null ? item.tongTien.toLocaleString() : "0"
            }</td>
            <td>${item.daThanhToan ? "Đã thanh toán" : "Chưa thanh toán"}</td>
            <td>${item.nguoiTao ?? ""}</td>
            <td>${
              item.ngayTao ? new Date(item.ngayTao).toLocaleString() : ""
            }</td>
            <td class="text-center">
              <a class="btn btn-info btn-sm me-1 xemHoaDonBtn" href="#" data-id="${
                item.hoaDonID ?? ""
              }">
                <i class="mdi mdi-eye"></i> Xem
              </a>
              <a class="btn btn-danger btn-sm me-1 huyHoaDonBtn" href="#" data-id="${
                item.hoaDonID ?? ""
              }">
                <i class="mdi mdi-delete"></i> Từ chối 
              </a>
            </td>
          `;
          tbody.appendChild(tr);
        });
      });
  }

  // Hiển thị modal chi tiết hóa đơn
  function showHoaDonModal(data) {
    document.getElementById("modalHoTen").textContent = data.hoTen ?? "";
    document.getElementById("modalEmail").textContent = data.email ?? "";
    document.getElementById("modalSoDienThoai").textContent =
      data.soDienThoai ?? "";
    document.getElementById("modalDiaChi").textContent = data.diaChi ?? "";
    document.getElementById("modalTongTien").textContent =
      data.tongTien != null ? data.tongTien.toLocaleString() : "0";
    document.getElementById("modalTrangThai").textContent =
      data.trangThaiDisplay ?? "";
    document.getElementById("modalPhuongThucTT").textContent =
      data.phuongThucThanhToanDisplay ?? "";
    document.getElementById("modalPhuongThucVC").textContent =
      data.phuongThucVanChuyenDisplay ?? "";
    document.getElementById("modalGhiChu").textContent = data.ghiChu ?? "";

    // Chi tiết sản phẩm
    const chiTietBody = document.getElementById("modalChiTietBody");
    chiTietBody.innerHTML = ""; // Xóa dữ liệu cũ trước khi hiển thị

    if (data.chiTietHoaDons && data.chiTietHoaDons.length > 0) {
      data.chiTietHoaDons.forEach((item) => {
        const tr = document.createElement("tr");
        tr.innerHTML = `
      <td class="chiTietTenGiay">${item.tenGiay ?? ""}</td>
      <td class="chiTietMauSac">${item.mauSac ?? ""}</td>
      <td class="chiTietSize">${item.size ?? ""}</td>
      <td class="chiTietSoLuong">${item.soLuong ?? 0}</td>
      <td class="chiTietDonGia">${item.donGia?.toLocaleString() ?? "0"}</td>
      <td class="chiTietCKPhanTram">${item.chietKhauPhanTram ?? 0}</td>
      <td class="chiTietCKTienMat">${item.chietKhauTienMat ?? 0}</td>
      <td class="chiTietTangKem">${item.isTangKem ? "Có" : "Không"}</td>
    `;
        chiTietBody.appendChild(tr);
      });
    }

    modalObj.show();
  }

  // Bắt sự kiện click
  tbody.addEventListener("click", function (e) {
    const xemBtn = e.target.closest(".xemHoaDonBtn");
    const huyBtn = e.target.closest(".huyHoaDonBtn");

    if (xemBtn) {
      const hoaDonID = xemBtn.dataset.id;
      if (!hoaDonID) return;
      fetch(`/Admin/HoaDon/GetHoaDonChiTiet?billID=${hoaDonID}`)
        .then((res) => res.json())
        .then((data) => {
          console.log("Dữ liệu hóa đơn chi tiết:", data); // <-- log để kiểm tra
          showHoaDonModal(data);
        })
        .catch((err) => console.error(err));
      return;
    }

    //huybtn

    if (huyBtn) {
      const huyhoaDonID = huyBtn.dataset.id;
      console.log("huyhoaDonID:", huyhoaDonID);

      if (!huyhoaDonID) return;
      if (!confirm("Bạn có chắc chắn muốn chuyển trạng thái hóa đơn này?")) return;
      fetch(`/Admin/HoaDon/HuyHoaDon`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ id: huyhoaDonID }),
      })
        .then((res) => res.json())
        .then((data) => {
          showToast(
            data.success ? data.message.message : data.message,
            data.success
          );
          loadHoaDon(); // Load lại bảng
        })
        .catch((err) => console.error(err));
      return;
    }
  });

  applyFilter.addEventListener("click", loadHoaDon);

  // Load mặc định hôm nay
  loadHoaDon();
  //bắt sự kiên chuyển trạng thái
  // Bắt sự kiện click vào trạng thái
  tbody.addEventListener("click", function (e) {
    const target = e.target.closest(".trangThaiCell");
    if (!target) return;

    const hoaDonID = target.dataset.id;
    console.log("hoaDonID:", hoaDonID);

    if (!confirm("Bạn có chắc chắn muốn thay đổi trạng thái đơn hàng này?"))
      return;

    fetch(`/Admin/HoaDon/UpdateTrangThai`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ HoaDonID: hoaDonID }),
    })
      .then((res) => res.json())
      .then((data) => {
        showToast(
          data.success ? data.message.message : data.message,
          data.success
        );
        loadHoaDon(); // Load lại bảng
      })
      .catch((err) => console.error(err));
  });
});

function getBadgeClassFromText(text) {
  switch (text?.trim()) {
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
