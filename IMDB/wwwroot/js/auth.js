document.addEventListener("DOMContentLoaded", function () {

    // 👁 Toggle Password
    document.querySelectorAll(".toggle-password").forEach(icon => {
        icon.addEventListener("click", function () {
            const input = this.parentElement.querySelector("input");
            if (input.type === "password") {
                input.type = "text";
                this.textContent = "🙈";
            } else {
                input.type = "password";
                this.textContent = "👁";
            }
        });
    });

    // 🔐 Password Strength (Register only)
    const passwordInput = document.querySelector("#Password");

    if (passwordInput) {

        const meter = document.querySelector(".strength-meter");
        const bar = document.querySelector(".strength-bar");
        const text = document.querySelector(".strength-text");

        passwordInput.addEventListener("input", function () {
            const validationSpan = document.querySelector("[data-valmsg-for='Password']");
            if (validationSpan) {
                validationSpan.textContent = "";
            }

            const val = this.value;
            let strength = 0;

            if (val.length > 5) strength++;
            if (/[A-Z]/.test(val)) strength++;
            if (/[0-9]/.test(val)) strength++;
            if (/[^A-Za-z0-9]/.test(val)) strength++;

            if (val.length > 0) {
                meter.classList.add("show");
                text.classList.add("show");
            } else {
                meter.classList.remove("show");
                text.classList.remove("show");
            }

            const levels = [
                { width: "0%", color: "", message: "" },
                { width: "25%", color: "#ff4d4d", message: "Weak password" },
                { width: "50%", color: "orange", message: "Medium password" },
                { width: "75%", color: "#00bfff", message: "Strong password" },
                { width: "100%", color: "limegreen", message: "Very Strong password" }
            ];

            const level = levels[strength];

            bar.style.width = level.width;
            bar.style.background = level.color;

            text.textContent = level.message;
            text.style.color = level.color;

            bar.classList.remove("strong-glow");

            if (strength === 4) {
                bar.classList.add("strong-glow");
            }

            confirmInput.dispatchEvent(new Event("input"));
        });
    }

    // 🔁 Confirm Password Live Match
    const confirmInput = document.querySelector("#ConfirmPassword");

    if (passwordInput && confirmInput) {

        const confirmText = document.querySelector(".confirm-text");

        confirmInput.addEventListener("input", function () {

            const passwordValue = passwordInput.value;
            const confirmValue = confirmInput.value;

            confirmText.classList.remove("match-success", "match-error");
            confirmInput.classList.remove("input-error", "input-success");

            if (confirmValue.length === 0) {
                confirmText.textContent = "";
                confirmText.classList.remove("show");
                return;
            }

            confirmText.classList.add("show");

            if (passwordValue === confirmValue) {

                confirmText.textContent = "✔ Passwords match";
                confirmText.classList.add("match-success");
                confirmInput.classList.add("input-success");

            } else {

                confirmText.textContent = "✖ Passwords do not match";
                confirmText.classList.add("match-error");
                confirmInput.classList.add("input-error");

            }
        });
    }

});
