document.addEventListener('DOMContentLoaded', function () {
    console.log("JS validation loaded");

    document.querySelectorAll('input[type="number"]').forEach(input => {
        // Chặn gõ dấu trừ
        input.addEventListener('keydown', function (e) {
            const allowedKeys = [
                'ArrowUp', 'ArrowDown', 'ArrowLeft', 'ArrowRight',
                'Backspace', 'Delete', 'Tab'
            ];
            if (allowedKeys.includes(e.key)) return;

            if (!/^\d$/.test(e.key)) {
                e.preventDefault();
            }
        });

        // Chặn paste số âm
        input.addEventListener('paste', function (e) {
            let pasteData = (e.clipboardData || window.clipboardData).getData('text');
            if (!/^\d+$/.test(pasteData)) {
                e.preventDefault();
            }
        });

        // Chặn scroll làm giá trị âm
        input.addEventListener('input', function () {
            if (this.value < 0) {
                this.value = 0;
            }
        });
    });
});