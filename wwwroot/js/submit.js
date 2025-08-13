document.addEventListener("DOMContentLoaded", () => {
    const forms = document.querySelectorAll("form");
    forms.forEach(form => {
        form.addEventListener("submit", function(e) {
            console.log("test")
            e.preventDefault(); // ngăn submit mặc định
            if (!validateForm(form)) return; // nếu lỗi → dừng

            showToast("Form hợp lệ, đang submit...", true);

            // Thực hiện submit sau 1-2 giây để kịp nhìn toast
            setTimeout(() => {
                form.submit(); // submit thủ công
            }, 1000);
        });
    });
});
