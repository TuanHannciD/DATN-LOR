document.addEventListener("DOMContentLoaded", () => {
  // Khi modal mở -> gọi API lấy danh sách sản phẩm đã xóa
  const removeBtn = document.getElementById("openModalRemoveBtn");
  const modalEl = document.getElementById("isRemoveModal"); // element modal
  const removeModal = new bootstrap.Modal(modalEl); // bootstrap modal object

  document
    .getElementById("openModalRemoveBtn")
    .addEventListener("click", async function (e) {
      e.preventDefault();
      removeModal.show();
      // Lấy controller name từ attribute data trên nút (không phải modal object)
      const entity = removeBtn.dataset.entity;
      getAllData(entity);
    });
});

async function getAllData(entity) {
  try {
    const res = await fetch(`/Admin/${entity}/GetAllDelete`);
    if (!res.ok) {
      showToast("Không thể lấy dữ liệu đã xóa.", false);
      return;
    }
    const data = await res.json();

    //Xử lý dữ liệu tra ra dựa trên entity
    console.log("Switch case");
    console.log("data:", data);

    switch (entity) {
      case "Giay":
        renderDeletedShoes(data, entity);
        break;
      case "ChiTietGiay":
        renderDeletedChiTietGiay(data, entity);
        break;
      case "ThuongHieu":
        renderDeletedBranch(data, entity);
        break;
      case "ChatLieu":
        renderDeletedMaterial(data, entity);
        break;
      case "DanhMuc":
        renderDeletedDanhMuc(data, entity);
        break;
      case "MauSac":
        renderDeletedMauSac(data, entity);
        break;
      case "KichThuoc":
        renderDeletedKichThuoc(data, entity);
        break;
    }
    showToast("Đã mở danh sách đã xóa", true);
  } catch (err) {
    console.error(err);
  }
}

// hàm render giay
function renderDeletedShoes(data, entity) {
  // Lấy tbody trong modal
  const tbody = document.getElementById("deletedProductsBody");
  if (!tbody) return;

  // Xóa nội dung cũ
  tbody.innerHTML = "";

  if (!data.success || !data.data || data.data.length === 0) {
    tbody.innerHTML = `<tr><td colspan="6" class="text-center">Không có sản phẩm nào</td></tr>`;
    return;
  }

  // Duyệt qua các sản phẩm
  data.data.forEach((item) => {
    const g = item.giay;
    const ngayXoa = item.ngayCapNhat
      ? new Date(item.ngayCapNhat).toLocaleString()
      : "";
    const tongSoLuong = item.tongSoLuong ?? 0;
    const ten = g.tenGiay;

    // Tạo row
    const row = document.createElement("tr");
    row.innerHTML = `
      <td>${ten}</td>
      <td>${g.maGiayCode}</td>
      <td>${g.moTa ?? ""}</td>
      <td><span class="badge bg-light text-dark">${ngayXoa}</span></td>
      <td>${item.nguoiCapNhat ?? ""}</td>
      <td>${tongSoLuong}</td>
      <td class="text-center">
        <button class="btn btn-success btn-sm me-1" onclick="restore('${
          g.shoeID
        }','${entity}','${ten}')">
          <i class="mdi mdi-restore"></i> Khôi phục
        </button>
      </td>
    `;

    tbody.appendChild(row);
  });
  console.log("Shoes:", data);
}

//Hàm render Thương hiệu
function renderDeletedBranch(data, entity) {
  // Lấy tbody trong modal
  const tbody = document.getElementById("deletedProductsBody");
  if (!tbody) return;

  // Xóa nội dung cũ
  tbody.innerHTML = "";

  if (!data.success || !data.data || data.data.length === 0) {
    tbody.innerHTML = `<tr><td colspan="6" class="text-center">Không có sản phẩm nào</td></tr>`;
    return;
  }

  // Duyệt qua các sản phẩm
  data.data.forEach((item) => {
    const ngayXoa = item.ngayCapNhat
      ? new Date(item.ngayCapNhat).toLocaleString()
      : "";
    const ten = item.tenThuongHieu;

    // Tạo row
    const row = document.createElement("tr");
    row.innerHTML = `
      <td>${ten}</td>
      <td>${item.ThuongHieu}</td>
      <td><span class="badge bg-light text-dark">${ngayXoa}</span></td>
      <td>${item.nguoiCapNhat ?? ""}</td>
      <td class="text-center">
        <button class="btn btn-success btn-sm me-1" onclick="restore('${
          item.brandID
        }','${entity}','${ten}')">
          <i class="mdi mdi-restore"></i> Khôi phục
        </button>
      </td>
    `;

    tbody.appendChild(row);
  });
  console.log("Thương hiệu:", data);
}

//Hàm render Chất Liệu
function renderDeletedMaterial(data, entity) {
  // Lấy tbody trong modal
  const tbody = document.getElementById("deletedProductsBody");
  if (!tbody) return;

  // Xóa nội dung cũ
  tbody.innerHTML = "";

  if (!data.success || !data.data || data.data.length === 0) {
    tbody.innerHTML = `<tr><td colspan="6" class="text-center">Không có sản phẩm nào</td></tr>`;
    return;
  }

  // Duyệt qua các sản phẩm
  data.data.forEach((item) => {
    const ngayXoa = item.ngayCapNhat
      ? new Date(item.ngayCapNhat).toLocaleString()
      : "";
    const ten = item.tenChatLieu;

    // Tạo row
    const row = document.createElement("tr");
    row.innerHTML = `
      <td>${ten}</td>
      <td>${item.maChatLieuCode}</td>
      <td><span class="badge bg-light text-dark">${ngayXoa}</span></td>
      <td>${item.nguoiCapNhat ?? ""}</td>
      <td class="text-center">
        <button class="btn btn-success btn-sm me-1" onclick="restore('${
          item.materialID
        }','${entity}','${ten}')">
          <i class="mdi mdi-restore"></i> Khôi phục
        </button>
      </td>
    `;

    tbody.appendChild(row);
  });
  console.log("Thương hiệu:", data);
}

//Hàm render màu sắc
function renderDeletedMauSac(data, entity) {
  // Lấy tbody trong modal
  const tbody = document.getElementById("deletedProductsBody");
  if (!tbody) return;

  // Xóa nội dung cũ
  tbody.innerHTML = "";

  if (!data.success || !data.data || data.data.length === 0) {
    tbody.innerHTML = `<tr><td colspan="6" class="text-center">Không có sản phẩm nào</td></tr>`;
    return;
  }

  // Duyệt qua các sản phẩm
  data.data.forEach((item) => {
    const ngayXoa = item.ngayCapNhat
      ? new Date(item.ngayCapNhat).toLocaleString()
      : "";
    const ten = item.tenMau;

    // Tạo row
    const row = document.createElement("tr");
    row.innerHTML = `
      <td>${ten}</td>
      <td>${item.maMau}</td>
      <td><span class="badge bg-light text-dark">${ngayXoa}</span></td>
      <td>${item.nguoiCapNhat ?? ""}</td>
      <td class="text-center">
        <button class="btn btn-success btn-sm me-1" onclick="restore('${
          item.colorID
        }','${entity}','${ten}')">
          <i class="mdi mdi-restore"></i> Khôi phục
        </button>
      </td>
    `;

    tbody.appendChild(row);
  });
}
//Hàm render kích thước
function renderDeletedKichThuoc(data, entity) {
  // Lấy tbody trong modal
  const tbody = document.getElementById("deletedProductsBody");
  if (!tbody) return;

  // Xóa nội dung cũ
  tbody.innerHTML = "";

  if (!data.success || !data.data || data.data.length === 0) {
    tbody.innerHTML = `<tr><td colspan="6" class="text-center">Không có kích thước nào</td></tr>`;
    return;
  }

  // Duyệt qua các sản phẩm
  data.data.forEach((item) => {
    const ngayXoa = item.ngayCapNhat
      ? new Date(item.ngayCapNhat).toLocaleString()
      : "";
    const ten = item.tenKichThuoc;

    // Tạo row
    const row = document.createElement("tr");
    row.innerHTML = `
      <td>${ten}</td>
      <td>${item.maKichThuocCode}</td>
      <td><span class="badge bg-light text-dark">${ngayXoa}</span></td>
      <td>${item.nguoiCapNhat ?? ""}</td>
      <td class="text-center">
        <button class="btn btn-success btn-sm me-1" onclick="restore('${
          item.sizeID
        }','${entity}','${ten}')">
          <i class="mdi mdi-restore"></i> Khôi phục
        </button>
      </td>
    `;

    tbody.appendChild(row);
  });
}

//Hàm render dnah mục
function renderDeletedDanhMuc(data, entity) {
  // Lấy tbody trong modal
  const tbody = document.getElementById("deletedProductsBody");
  if (!tbody) return;

  // Xóa nội dung cũ
  tbody.innerHTML = "";

  if (!data.success || !data.data || data.data.length === 0) {
    tbody.innerHTML = `<tr><td colspan="6" class="text-center">Không có sản phẩm nào</td></tr>`;
    return;
  }

  // Duyệt qua các sản phẩm
  data.data.forEach((item) => {
    const ngayXoa = item.ngayCapNhat
      ? new Date(item.ngayCapNhat).toLocaleString()
      : "";
    const ten = item.tenDanhMuc;

    // Tạo row
    const row = document.createElement("tr");
    row.innerHTML = `
      <td>${ten}</td>
      <td>${item.maDanhMucCode}</td>
      <td><span class="badge bg-light text-dark">${ngayXoa}</span></td>
      <td>${item.nguoiCapNhat ?? ""}</td>
      <td class="text-center">
        <button class="btn btn-success btn-sm me-1" onclick="restore('${
          item.categoryID
        }','${entity}','${ten}')">
          <i class="mdi mdi-restore"></i> Khôi phục
        </button>
      </td>
    `;

    tbody.appendChild(row);
  });
  console.log("Thương hiệu:", data);
}
//Hàm render ChiTietGiay
function renderDeletedChiTietGiay(data, entity) {
  const tbody = document.getElementById("deletedProductsBody");
  if (!tbody) return;

  tbody.innerHTML = "";
  if (!data.success || !data.data || data.data.length == 0) {
    tbody.innerHTML = `<tr><td colspan="6" class="text-center">Không có sản phẩm chi tiết nào bị xóa</td></tr>`;
  }

  data.data.forEach((item) => {
    console.log("item:", item);

    const giaCT = item.gia ?? "Không lấy được giá";
    const soLuongCT = item.soLuong ?? "Không lấy được số lượng";
    const ten = item.tenGiay;
    const row = document.createElement("tr");
    row.innerHTML = `
      <td>${ten}</td>
      <td>${item.tenKichThuoc}</td>
      <td>${item.tenMau}</td>
      <td>${item.tenChatLieu}</td>
      <td>${item.tenThuongHieu}</td>
      <td>${item.tenDanhMuc}</td>
      <td>${soLuongCT}</td>
      <td>${giaCT}</td>
      <td class="text-center">
        <button class="btn btn-success btn-sm me-1" onclick="restore('${item.shoeDetailID}','${entity}','${ten}')">
          <i class="mdi mdi-restore"></i> Khôi phục
        </button>
      </td>
    `;

    tbody.appendChild(row);
  });
}

// Gọi API khôi phục sản phẩm
function restore(id, entity, ten) {
  console.log("Entity callback", entity);
  if (!confirm(`Bạn có chắc muốn khôi phục ${ten}?`)) return;

  fetch(`/Admin/${entity}/Restore/` + id, { method: "POST" })
    .then((res) => res.json())
    .then((rs) => {
      if (rs.success) {
        showToast("Khôi phục thành công", true);
        getAllData(entity);
      } else {
        alert("Lỗi: " + rs.message);
      }
    });
}
