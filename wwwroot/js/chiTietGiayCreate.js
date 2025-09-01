$(document).ready(function () {
  // =========================
  // 1️⃣ Select2
  // =========================
  $(".select2").select2({
    placeholder: "Chọn",
    allowClear: true,
    width: "100%",
  });

  // =========================
  // 2️⃣ Quick Add cho select
  // =========================
  function quickAdd(btn, panel, input, select, url, dataBuilder) {
    // 1️⃣ Bật/tắt panel khi click nút "+"
    console.log("btn", btn);
    console.log("panel", panel);
    console.log("input", input);
    console.log("select", select);
    console.log("url", url);

    $(btn).click(() => $(panel).toggleClass("d-none"));

    // 2️⃣ Khi click "Lưu" trong panel
    $(panel)
      .find(".btn-primary")
      .click(() => {
        let val = $(input).val().trim();
        if (!val) {
          alert("Vui lòng nhập tên");
          return;
        }

        // Lấy anti-forgery token
        var token = $('input[name="__RequestVerificationToken"]').val();

        // 3️⃣ Gửi AJAX POST lên server
        $.ajax({
          url: url,
          type: "POST",
          headers: { RequestVerificationToken: token },
          data: JSON.stringify(dataBuilder(val)), // object phải đúng với model backend
          contentType: "application/json",
          success: function (res) {
            if (res.success) {
              // 4️⃣ Thêm option mới vào select và chọn nó
              var newOption = new Option(
                res.data.text,
                res.data.id,
                true,
                true
              );
              $(select).append(newOption).trigger("change");

              // 5️⃣ Reset panel
              $(input).val("");
              $(panel).addClass("d-none");

              showToast(res.message, true); // hoặc dùng toastr
            } else {
              showToast(res.message, false);
            }
          },
          error: function (xhr, status, error) {
            alert("Lỗi khi thêm: " + error);
          },
        });
      });

    // 6️⃣ Khi click "Hủy"
    $(panel)
      .find(".btn-secondary")
      .click(() => {
        $(input).val("");
        $(panel).addClass("d-none");
      });
  }

  // 7️⃣ Gọi hàm cho từng select
  quickAdd(
    "#btnAddShoe",
    "#quickAddShoe",
    "#newShoe",
    "#ShoeID",
    "/Admin/ChiTietGiay/QuickAdd",
    (val) => ({ TenGiay: val })
  );
  quickAdd(
    "#btnAddMaterial",
    "#quickAddMaterial",
    "#newMaterial",
    "#MaterialID",
    "/Admin/ChiTietGiay/QuickAdd",
    (val) => ({ TenChatLieu: val })
  );
  quickAdd(
    "#btnAddBrand",
    "#quickAddBrand",
    "#newBrand",
    "#BrandID",
    "/Admin/ChiTietGiay/QuickAdd",
    (val) => ({ TenHang: val })
  );
  quickAdd(
    "#btnAddCategory",
    "#quickAddCategory",
    "#newCategory",
    "#CategoryID",
    "/Admin/ChiTietGiay/QuickAdd",
    (val) => ({ TenDanhMuc: val })
  );

  // =========================
  //  Thêm/Xóa chi tiết giày
  // =========================
  function setupRowUpload(row) {
    const fileInput = row.querySelector(".imageInput");
    const hiddenInput = row.querySelector(".imageUrls");
    const previewDiv = row.querySelector(".previewDiv");

    fileInput.addEventListener("change", async function () {
      const files = Array.from(this.files);
      previewDiv.innerHTML = ""; // Xóa preview cũ
      let urls = [];

      for (const file of files) {
        try {
          const url = await uploadToCloudinary(file); // trả về URL thực
          urls.push(url);
          console.log("URl:", url);

          const img = document.createElement("img");
          img.src = url;
          img.style.height = "80px";
          img.style.objectFit = "cover";
          img.style.border = "1px solid #ddd";
          img.style.borderRadius = "4px";
          img.style.marginRight = "4px";
          previewDiv.appendChild(img);
        } catch (err) {
          console.error("Upload ảnh thất bại:", err);
        }
      }

      hiddenInput.value = JSON.stringify(urls);
    });
  }

  // Setup row đầu tiên
  const firstRow = document.querySelector(".chiTietRow");
  setupRowUpload(firstRow);

  $("#addChiTiet").click(function () {
    const lastRow = $(".chiTietRow").last();
    const newRow = lastRow.clone(false, false);

    // copy value, nhưng bỏ qua input[type=file]
    newRow.find("input, select, textarea").each(function (index) {
      const $oldField = lastRow.find("input, select, textarea").eq(index);

      if ($oldField.attr("type") === "file") {
        $(this).val(""); // reset file input
      } else {
        $(this).val($oldField.val());
      }
    });

    $("#chiTietContainer").append(newRow);

    // setup upload cho row mới
    setupRowUpload(newRow[0]);
  });

  // Xóa row
  $("#chiTietContainer").on("click", ".removeRow", function () {
    if ($("#chiTietContainer .chiTietRow").length > 1) {
      $(this).closest(".chiTietRow").remove();
    }
  });
});
document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("createForm");
  // Call api từ BE\
  $("#saveProduct").click(function (e) {
    e.preventDefault();
    let chiTietImages = [];
    $("#chiTietContainer .chiTietRow").each(function () {
      let row = $(this);
      let shoeId = row.find("select[name='ShoeID']").val();
      let sizeId = row.find("select[name='SizeID']").val();
      let colorId = row.find("select[name='ColorID']").val();
      let images = row.find(".imageUrls").val(); // JSON string

      chiTietImages.push({
        shoeId,
        sizeId,
        colorId,
        images: images ? JSON.parse(images) : [],
      });
    });

    const data = {
      ShoeID: $("#ShoeID").val(),
      MaterialID: $("#MaterialID").val(),
      BrandID: $("#BrandID").val(),
      CategoryID: $("#CategoryID").val(),
      SoLuong: parseFloat($("#SoLuong").val()) || 0,
      Gia: parseFloat($("#Gia").val()) || 0,
      ChiTietImages: chiTietImages,
    };
    console.log("Data gửi đi:", data);

    $.ajax({
      url: "/Admin/ChiTietGiay/Create",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify(data),
      success: function (res) {
        if (res.success) {
          showToast(res.message, "success");
          form.reset();
        } else {
          if (res.errors) {
            showToast("Có lỗi trong form", "danger");
          } else {
            showToast(res.message, "danger");
          }
        }
      },
      error: function (err) {
        showToast("Lỗi hệ thống", "danger");
        console.error(err);
      },
    });
  });
});
