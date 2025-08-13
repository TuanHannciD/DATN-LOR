function validateForm(form) {
    let valid = true;
    const inputs = form.querySelectorAll("[data-validate]");

    inputs.forEach(input => {
        const rules = input.dataset.validate.split("|");
        const value = input.value.trim();
        const labelName = input.dataset.label || "Trường này";

        rules.forEach(rule => {
            if (rule === "required" && value === "") {
                showToast(`${labelName} không được để trống!`, false);
                valid = false;
            }
            if (rule.startsWith("min:")) {
                const minLen = parseInt(rule.split(":")[1]);
                if (value.length < minLen) {
                    showToast(`${labelName} phải ít nhất ${minLen} ký tự!`, false);
                    valid = false;
                }
            }
        });
    });

    return valid;
}
