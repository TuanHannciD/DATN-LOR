document.addEventListener("DOMContentLoaded", function () {
    var lastClickedBtn = null;

    // Ghi nhớ nút submit cuối cùng được bấm
    document.querySelectorAll("button[type=submit], input[type=submit]")
        .forEach(function (btn) {
            btn.addEventListener("click", function () {
                lastClickedBtn = btn;
            });
        });

    // Xử lý submit cho tất cả form không phải ajax
    document.querySelectorAll("form:not(.ajax):not([data-bound='1'])")
        .forEach(function (form) {
            form.dataset.bound = "1"; // tránh bind nhiều lần

            form.addEventListener("submit", function (e) {
                // Nếu nút submit có data-skip-validate => bỏ qua kiểm tra
                if (lastClickedBtn && lastClickedBtn.getAttribute("data-skip-validate") === "true") {
                    lastClickedBtn = null; // reset
                    return; // Cho phép submit luôn
                }

                e.preventDefault();

                // Gọi hàm validate (bạn định nghĩa sẵn)
                if (typeof validateForm === "function" && !validateForm(form)) {
                    return; // Dừng nếu không hợp lệ
                }

                if (typeof showToast === "function") {
                    showToast("Form hợp lệ, đang submit...", true);
                }

                // Delay 1s để user thấy thông báo
                setTimeout(function () {
                    form.submit();
                }, 1000);
            });
        });
});
