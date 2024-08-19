
console.log("toggle.js loaded");

document.addEventListener("DOMContentLoaded", function () {
    const themeToggleBtn = document.getElementById('theme-toggle');
    const currentTheme = localStorage.getItem('theme');

    if (currentTheme === 'dark') {
        document.body.classList.add('dark-mode');
        themeToggleBtn.textContent = '☀️ Light Mode';
    }

    themeToggleBtn.addEventListener('click', function () {
        document.body.classList.toggle('dark-mode');
        let theme = 'light';
        if (document.body.classList.contains('dark-mode')) {
            theme = 'dark';
            themeToggleBtn.textContent = '☀️ Light Mode';
        } else {
            themeToggleBtn.textContent = '🌙 Dark Mode';
        }
        localStorage.setItem('theme', theme);
    });
});
