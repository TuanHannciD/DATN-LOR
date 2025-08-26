// banhangtaiquay.js - File chính, ES6 module
import { tinhThanhTienDong, tinhTongTienGioHang } from "./modules/cart.js";
import {
  openDiscountModal,
  setDiscountType,
  handleDiscountSave,
  handleTangKemCheckbox,
  handleQuickValue,
} from "./modules/discount-modal.js";
import {
  openOrderDiscountModal,
  setOrderDiscountType,
  handleOrderDiscountSave,
  handleOrderDiscountQuickValue,
} from "./modules/order-discount-modal.js";
import {
  searchCustomer,
  renderCustomerDropdown,
  selectCustomer,
} from "./modules/customer-search.js";
import {
  tinhThanhTienSauGiam,
  fillInvoiceSummary,
} from "./modules/order-summary.js";
import { updateCart } from "./modules/update-cart.js";

let currentDiscountRow = null;

$(document).ready(function () {
  console.log("✅ Document ready");
  $("#search-input").on("input", function () {
    var keyword = $(this).val().trim();
    if (keyword.length === 0) {
      $("#search-dropdown").removeClass("show").empty();
      return;
    }
    $.get(
      "/Admin/BanHangTaiQuay/SearchSanPham",
      { keyword: keyword },
      function (data) {
        if (data && data.length > 0) {
          let html = "";
          data.forEach(function (sp) {
            html += `
            <button type="button" class="dropdown-item" data-id="${
              sp.shoeDetailID
            }">
            <div><strong>${
              sp.tenSp
            }</strong> <span class="text-success ms-2">${sp.gia.toLocaleString()} VNĐ</span></div>
            <div class="text-muted">Màu: ${sp.mauSac} | Size: ${
              sp.kichThuoc
            }</div>
            <div class="text-muted">Thương hiệu: ${
              sp.thuongHieu
            } | Chất liệu: ${sp.chatLieu}</div>
            <div class="text-muted">Danh mục: ${sp.danhMuc}</div>
            <div class="text-muted">Số lượng kho: ${sp.soLuong}</div>
            </button>`;
          });

          $("#search-dropdown").html(html).addClass("show");
        } else {
          $("#search-dropdown")
            .html(
              '<div class="dropdown-item text-muted">Không tìm thấy sản phẩm</div>'
            )
            .addClass("show");
        }
      }
    );
  });

  // // Khi click vào 1 sản phẩm trong dropdown
  // $(document).on("click", "button.dropdown-item", function (e) {
  //   e.preventDefault(); // chặn link nhảy trang

  //   let shoeDetailId = $(this).data("id");
  //   console.log("Click vào sản phẩm có ID:", shoeDetailId);

  //   updateCart(shoeDetailId, "add");
  // });

  $("#phuongthuc-thanh-toan").select2({
    width: "100%",
    templateResult: function (state) {
      if (!state.id) return state.text;
      let icon = "";
      switch (parseInt(state.id)) {
        case 0:
          icon = '<i class="mdi mdi-cash me-1"></i>';
          break; // Tiền mặt
        case 1:
          icon = '<i class="mdi mdi-bank-transfer me-1"></i>';
          break; // Chuyển khoản
        case 2:
          icon = '<i class="mdi mdi-credit-card me-1"></i>';
          break; // Thẻ tín dụng
        case 3:
          icon = '<i class="mdi mdi-cellphone me-1"></i>';
          break; // Ví điện tử
      }
      return $("<span>" + icon + state.text + "</span>");
    },
    templateSelection: function (state) {
      if (!state.id) return state.text;
      let icon = "";
      switch (parseInt(state.id)) {
        case 0:
          icon = '<i class="mdi mdi-cash me-1"></i>';
          break;
        case 1:
          icon = '<i class="mdi mdi-bank-transfer me-1"></i>';
          break;
        case 2:
          icon = '<i class="mdi mdi-credit-card me-1"></i>';
          break;
        case 3:
          icon = '<i class="mdi mdi-cellphone me-1"></i>';
          break;
      }
      return $("<span>" + icon + state.text + "</span>");
    },
    escapeMarkup: function (m) {
      return m;
    },
  });
  $("#phuongthuc-van-chuyen").select2({
    width: "100%",
    templateResult: function (state) {
      if (!state.id) return state.text;
      let icon = "";
      switch (parseInt(state.id)) {
        case 0:
          icon = '<i class="mdi mdi-truck-fast me-1"></i>';
          break; // Giao hàng nhanh
        case 1:
          icon = '<i class="mdi mdi-package-variant me-1"></i>';
          break; // Giao hàng tiết kiệm
        case 2:
          icon = '<i class="mdi mdi-store me-1"></i>';
          break; // Tự đến lấy
      }
      return $("<span>" + icon + state.text + "</span>");
    },
    templateSelection: function (state) {
      if (!state.id) return state.text;
      let icon = "";
      switch (parseInt(state.id)) {
        case 0:
          icon = '<i class="mdi mdi-truck-fast me-1"></i>';
          break;
        case 1:
          icon = '<i class="mdi mdi-package-variant me-1"></i>';
          break;
        case 2:
          icon = '<i class="mdi mdi-store me-1"></i>';
          break;
      }
      return $("<span>" + icon + state.text + "</span>");
    },
    escapeMarkup: function (m) {
      return m;
    },
  });
  //
  // Ở file chính (banhangtaiquay.js)
  // Gắn sự kiện click cho tất cả nút update cart (tăng/giảm/xóa/Chọn)
  // Gắn sự kiện click cho tất cả nút update cart (tăng/giảm/xóa/Chọn)
  document.addEventListener("click", (e) => {
    // Tìm nút nhấn gần nhất có class .btn-update-cart
    const btn = e.target.closest(".btn-update-cart");
    if (!btn) return;

    const shoeDetailId = btn.getAttribute("data-id");
    const actionType = btn.getAttribute("data-action");

    if (!shoeDetailId || !actionType) return;

    // Gọi updateCart, truyền btn để biết card nào cần chèn controls
    updateCart(shoeDetailId, actionType, btn);
  });

  // Khi click vào sản phẩm trong dropdown
  $("#search-dropdown").on("click", ".dropdown-item", function () {
    var shoeDetailID = $(this).data("id");
    $("#hidden-shoeDetailId").val(shoeDetailID);
    $("#add-to-cart-form").submit();
    $("#search-dropdown").removeClass("show").empty();
    $("#search-input").val("");
  });

  // Ẩn dropdown khi click ra ngoài
  $(document).on("click", function (e) {
    if (!$(e.target).closest("#search-input, #search-dropdown").length) {
      $("#search-dropdown").removeClass("show").empty();
    }
  });
  // Tìm kiếm khách hàng
  $("#search-khachhang").on("input", function () {
    var keyword = $(this).val().trim();
    if (keyword.length < 1) {
      $("#dropdown-khachhang").removeClass("show").empty();
      return;
    }
    searchCustomer(keyword, renderCustomerDropdown);
  });
  // Chọn khách hàng
  $("#dropdown-khachhang").on("click", ".dropdown-item", function () {
    selectCustomer($(this));
  });
  // Ẩn dropdown khi click ra ngoài
  $(document).on("click", function (e) {
    if (!$(e.target).closest("#search-khachhang, #dropdown-khachhang").length) {
      $("#dropdown-khachhang").removeClass("show").empty();
    }
  });

  // Giỏ hàng
  tinhThanhTienDong();
  tinhTongTienGioHang();
  fillInvoiceSummary(undefined, 0); // Lần đầu load, chưa có giảm giá hóa đơn, phí vận chuyển 0

  // Modal giảm giá sản phẩm
  $(document).on("click", ".bhq-btn-discount", function (e) {
    e.stopPropagation();
    currentDiscountRow = $(this).closest("tr");
    // Lấy lại giá trị hiện tại nếu có để fill vào modal
    let isTang = currentDiscountRow.data("tangkem") == 1;
    let chietKhauPhanTram = currentDiscountRow.data("chietkhau-phantram");
    let chietKhauTienMat = currentDiscountRow.data("chietkhau-tienmat");
    // Reset modal trước khi fill
    $("#discount-tangkem").prop("checked", isTang);
    if (isTang) {
      setDiscountType("percent");
      $("#discount-value").val(0).prop("disabled", true);
      $(".btn-toggle-type").prop("disabled", true);
      $(".btn-quick-value").prop("disabled", true);
    } else if (chietKhauPhanTram && chietKhauPhanTram > 0) {
      setDiscountType("percent");
      $("#discount-value").val(chietKhauPhanTram).prop("disabled", false);
      $(".btn-toggle-type").prop("disabled", false);
      $(".btn-quick-value").prop("disabled", false);
    } else if (chietKhauTienMat && chietKhauTienMat > 0) {
      setDiscountType("amount");
      $("#discount-value").val(chietKhauTienMat).prop("disabled", false);
      $(".btn-toggle-type").prop("disabled", false);
      $(".btn-quick-value").prop("disabled", false);
    } else {
      setDiscountType("percent");
      $("#discount-value").val(0).prop("disabled", false);
      $(".btn-toggle-type").prop("disabled", false);
      $(".btn-quick-value").prop("disabled", false);
    }
    setTimeout(() => {
      $("#discount-value").focus();
    }, 300);
    openDiscountModal();
  });
  $(document).on("click", ".btn-toggle-type", function () {
    setDiscountType($(this).data("type"));
  });

  // Hàm hiển thị lỗi validate cho input
  function showInputError(inputSelector, message) {
    $(inputSelector).addClass("is-invalid");
    if ($(inputSelector).next(".invalid-feedback").length === 0) {
      $(inputSelector).after(
        '<div class="invalid-feedback">' + message + "</div>"
      );
    } else {
      $(inputSelector).next(".invalid-feedback").text(message);
    }
  }
  function clearInputError(inputSelector) {
    $(inputSelector).removeClass("is-invalid");
    $(inputSelector).next(".invalid-feedback").remove();
  }

  // Validate modal giảm giá sản phẩm
  handleDiscountSave(function (data) {
    clearInputError("#discount-reason");
    clearInputError("#discount-value");
    // Validate lý do

    // Validate giá trị giảm giá
    const value = parseFloat($("#discount-value").val()) || 0;
    const isPercent = $('.btn-toggle-type[data-type="percent"]').hasClass(
      "btn-primary"
    );
    if (value < 0) {
      showInputError("#discount-value", "Giá trị giảm giá không được âm!");
      $("#discount-value").focus();
      return;
    }
    if (isPercent && value > 100) {
      showInputError(
        "#discount-value",
        "Giảm giá phần trăm không được vượt quá 100%!"
      );
      $("#discount-value").focus();
      return;
    }
    // Nếu tất cả trường đều rỗng/0/không tích, coi như xóa giảm giá
    const isRemoveDiscount =
      (!data.chietKhauPhanTram || data.chietKhauPhanTram == 0) &&
      (!data.chietKhauTienMat || data.chietKhauTienMat == 0) &&
      !data.isTang &&
      (!data.reason || data.reason.trim() === "");
    if (isRemoveDiscount) {
      if (!currentDiscountRow) return;
      let giaGoc = parseInt(currentDiscountRow.data("gia-goc")) || 0;
      let soLuong = parseInt(currentDiscountRow.data("so-luong")) || 1;
      currentDiscountRow.data("tangkem", 0);
      currentDiscountRow.data("gia-sau-giam", giaGoc);
      $.post(
        "/Admin/BanHangTaiQuay/UpdateDiscountCartItem",
        {
          cartDetailId: currentDiscountRow.data("cartdetailid"),
          chietKhauPhanTram: null,
          chietKhauTienMat: null,
          isTangKem: false,
          reason: "",
        },
        function (response) {
          let html =
            '<span class="fw-bold">' +
            giaGoc.toLocaleString("vi-VN") +
            "</span> VNĐ";
          currentDiscountRow.find(".thanh-tien-dong").html(html);
          currentDiscountRow.find(".bhq-cart-reason").text("");
          tinhTongTienGioHang();
          fillInvoiceSummary(undefined, 0);
          $("#discountModal").modal("hide");
          currentDiscountRow = null;
        }
      );
      return;
    }
    if (!data.reason || data.reason.trim().length < 3) {
      showInputError(
        "#discount-reason",
        "Lý do giảm giá phải có ít nhất 3 ký tự!"
      );
      $("#discount-reason").focus();
      return;
    }
    if (!currentDiscountRow) return;
    let giaGoc = parseInt(currentDiscountRow.data("gia-goc")) || 0;
    let soLuong = parseInt(currentDiscountRow.data("so-luong")) || 1;
    let giaSauGiam = giaGoc;
    if (data.isTang) {
      giaSauGiam = 0;
      currentDiscountRow.data("tangkem", 1);
    } else {
      currentDiscountRow.data("tangkem", 0);
      if (data.chietKhauPhanTram !== null) {
        giaSauGiam = Math.round(giaGoc * (1 - data.chietKhauPhanTram / 100));
      } else if (data.chietKhauTienMat !== null) {
        giaSauGiam = Math.max(0, giaGoc - data.chietKhauTienMat);
      }
    }
    currentDiscountRow.data("gia-sau-giam", giaSauGiam);
    // Gửi AJAX lưu vào DB
    $.post(
      "/Admin/BanHangTaiQuay/UpdateDiscountCartItem",
      {
        cartDetailId: currentDiscountRow.data("cartdetailid"),
        chietKhauPhanTram: data.chietKhauPhanTram,
        chietKhauTienMat: data.chietKhauTienMat,
        isTangKem: data.isTang,
        reason: data.reason,
      },
      function (response) {
        // Thành công: cập nhật lại thành tiền dòng, tổng tiền, phép tính hóa đơn, lý do
        let html = "";
        if (data.isTang) {
          html =
            '<span class="badge bg-success ms-2" style="font-size: 0.95em;">🎁 Tặng</span>';
        } else if (
          (data.chietKhauPhanTram && data.chietKhauPhanTram > 0) ||
          (data.chietKhauTienMat && data.chietKhauTienMat > 0)
        ) {
          html =
            '<span style="color:#888; font-size:0.95em; text-decoration:line-through;">' +
            giaGoc.toLocaleString("vi-VN") +
            "</span>";
          html +=
            '<span class="fw-bold text-danger" style="font-size:1em;"> ' +
            giaSauGiam.toLocaleString("vi-VN") +
            '</span> <span class="text-danger">VNĐ</span>';
        } else {
          html =
            '<span class="fw-bold">' +
            giaGoc.toLocaleString("vi-VN") +
            "</span> VNĐ";
        }
        currentDiscountRow.find(".thanh-tien-dong").html(html);
        // Cập nhật lại lý do trên UI
        currentDiscountRow.find(".bhq-cart-reason").text(data.reason || "");
        tinhTongTienGioHang();
        fillInvoiceSummary(undefined, 0);
        $("#discountModal").modal("hide");
        currentDiscountRow = null;
      }
    );
  });
  handleTangKemCheckbox();
  handleQuickValue();

  // Modal giảm giá hóa đơn
  $("#btn-modal-giamgia").on("click", function () {
    openOrderDiscountModal();
  });
  $(document).on("click", ".btn-toggle-type-giamgia", function () {
    setOrderDiscountType($(this).data("type"));
  });
  handleOrderDiscountQuickValue();
  let giamGiaHoaDon = { phanTram: 0, tienMat: 0, lyDo: "" };
  handleOrderDiscountSave(function (data) {
    clearInputError("#modal-lydo-giamgia");
    clearInputError("#modal-giamgia-value");
    if (!data.lyDo || data.lyDo.trim().length < 3) {
      showInputError(
        "#modal-lydo-giamgia",
        "Lý do giảm giá hóa đơn phải có ít nhất 3 ký tự!"
      );
      $("#modal-lydo-giamgia").focus();
      return;
    }
    const value = parseFloat($("#modal-giamgia-value").val()) || 0;
    const isPercent = $(
      '.btn-toggle-type-giamgia[data-type="percent"]'
    ).hasClass("btn-primary");
    if (value < 0) {
      showInputError("#modal-giamgia-value", "Giá trị giảm giá không được âm!");
      $("#modal-giamgia-value").focus();
      return;
    }
    if (isPercent && value > 100) {
      showInputError(
        "#modal-giamgia-value",
        "Giảm giá phần trăm không được vượt quá 100%!"
      );
      $("#modal-giamgia-value").focus();
      return;
    }
    giamGiaHoaDon = data;
    let summary = "";
    if (giamGiaHoaDon.phanTram > 0)
      summary += `Giảm ${giamGiaHoaDon.phanTram}%`;
    if (giamGiaHoaDon.tienMat > 0)
      summary +=
        (summary ? ", " : "") +
        `Giảm ${giamGiaHoaDon.tienMat.toLocaleString()} VNĐ`;
    if (giamGiaHoaDon.lyDo)
      summary += (summary ? ", " : "") + `Lý do: ${giamGiaHoaDon.lyDo}`;
    $("#giamgia-summary").text(summary);
    // Tính lại tổng tiền sau giảm
    tinhThanhTienSauGiam(giamGiaHoaDon);
    fillInvoiceSummary(giamGiaHoaDon, 0); // Cập nhật bảng phép tính trực quan hóa đơn
    $("#giamgiaModal").modal("hide");
  });
  // Tính tổng tiền sau giảm khi load trang
  tinhThanhTienSauGiam(giamGiaHoaDon);
  // Xử lý sự kiện click
  $(document).on("click", "#btn-thanhtoan", async function () {
    const selectedShippingMethod = $("#phuongthuc-van-chuyen").val();
    const selecttedPaymentMethod = $("#phuongthuc-thanh-toan").val();
    const tongTienText = $("#thanh-tien-sau-giam").text();
    const khachhangId = $("#selected-khachhang-id").val();
    const tongTien = parseInt(tongTienText.replace(/[^0-9]/g, "")) || 0;

    console.log("[FE] Bắt đầu xử lý thanh toán...");
    console.log("[FE] Phương thức vận chuyển:", selectedShippingMethod);
    console.log("[FE] Phương thức thanh toán:", selecttedPaymentMethod);
    console.log("[FE] Tổng tiền:", tongTien);
    console.log("[FE] ID khách hàng:", khachhangId);
    try {
      // ajax tạo hóa đơn thanh toán trước
      if (khachhangId === "") {
        showToast("Vui lòng chọn khách hàng trước khi thanh toán!", "error");
        return;
      }
      const createOrderResponse = await fetch("/Admin/HoaDon/CreateHoaDon", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          userID: khachhangId || null, // Nếu không có khách hàng thì để null
          hinhThucThanhToan: selecttedPaymentMethod,
          hinhThucVanChuyen: selectedShippingMethod,
          giamGiaPhanTram: giamGiaHoaDon.phanTram,
          giamGiaTienMat: giamGiaHoaDon.tienMat,
          lyDo: giamGiaHoaDon.lyDo,
        }),
      });
      const createOrderData = await createOrderResponse.json(); // parse JSON trước

      if (!createOrderResponse.ok) {
        showToast(
          "Lỗi: " +
            (createOrderData.data?.message ||
              createOrderData.message ||
              "Không rõ nguyên nhân")
        );
        return;
      } else {
        showToast(createOrderData.message, "success");
      }
      var orderId = createOrderData.hoaDon.billID;
      if (!orderId) {
        showToast("Không nhận được ID hóa đơn từ server!", "error");
        return;
      }
      if (selecttedPaymentMethod == "1") {
        try {
          const response = await fetch("/Admin/Payment/CreatePayment", {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              tongTien,
              orderId,
              orderInfo: "Don hang test VNPay", // Thông tin đơn hàng
              bankCode: "VNBank", // Mã ngân hàng mặc định
              returnUrl: window.location.origin + "/Admin/Payment/VNPayReturn", // URL trả về sau thanh toán
            }),
          });
          if (!response.ok) {
            const errorHtml = await response.text();
            showToast("Lỗi từ server:" + errorHtml, "error");
            return;
          }
          let data;
          try {
            data = await response.json();
          } catch (parseErr) {
            showToast(
              "Lỗi: Phản hồi không hợp lệ (không phải JSON). Kiểm tra phía server.",
              error
            );
            return;
          }

          if (data && data.paymentUrl) {
            showToast("[VNPay] ✅ Mở URL thanh toán", "success");
            window.location.href = data.paymentUrl;
          } else {
            console.warn(
              "[VNPay] ⚠️ Không có paymentUrl trong phản hồi:",
              data
            );
            showToast("Không nhận được URL thanh toán từ server!", "error");
          }
        } catch (err) {
          showToast("Tạo đơn test thất bại: " + err.message, "error");
        }
      } else if (selecttedPaymentMethod == "0") {
        // Xử lý thanh toán bằng tiền mặt
        const confirmPayment = confirm(
          "Bạn có chắc chắn muốn thanh toán bằng tiền mặt không?"
        );
        if (confirmPayment) {
          const confirmdone = confirm("Xác nhận là đã thanh toan tiền mặt");
          if (confirmdone) {
            try {
              const response = await fetch("/Admin/HoaDon/XacnhanTienMat", {
                method: "POST",
                headers: {
                  "Content-Type": "application/x-www-form-urlencoded",
                },
                body: new URLSearchParams({
                  confirmdone: confirmdone,
                  orderId: orderId,
                }),
              });
              const responseText = await response.json();
              showToast(
                "[FE] ✅ Cập nhật trạng thái thanh toán:" + responseText,
                "success"
              );
              if (!response.ok) {
                showToast(
                  "Lỗi khi cập nhật trạng thái thanh toán: " +
                    responseText.message,
                  "error"
                );
                return;
              }
              showToast(responseText.message, "success");
              showToast("Thanh toán tiền mặt thành công!", "success");
              window.location.reload(); // Tải lại trang để cập nhật giỏ hàng và hóa đơn
            } catch (err) {
              showToast(
                "Thanh toán tiền mặt thất bại: " + err.message,
                "error"
              );
            }
          }
        }
      }
    } catch (error) {
      showToast("Lỗi khi tạo hóa đơn: " + error.message, "error");
    }
  });
});
// Xử lý khi VNPay redirect về cùng trang
document.addEventListener("DOMContentLoaded", () => {
  const params = new URLSearchParams(window.location.search);
  const responseCode = params.get("vnp_ResponseCode");
  const orderId = params.get("vnp_TxnRef");
  const amount = params.get("vnp_Amount");

  if (responseCode) {
    if (responseCode === "00") {
      showToast(
        `✅ Thanh toán thành công!<br>
                Hóa đơn: ${orderId}<br>
                Số tiền: ${Number(amount).toLocaleString("vi-VN")} VNĐ`,
        "success"
      );
    } else {
      showToast(`Thanh toán đơn ${orderId} thất bại: ${responseCode}`, "error");
    }

    // Xóa query string để tránh reload lặp lại toast
    window.history.replaceState({}, document.title, window.location.pathname);
  }
});
