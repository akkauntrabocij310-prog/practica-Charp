// BookLibrary — site.js
// Клиентская валидация формы + небольшие UX-улучшения

document.addEventListener('DOMContentLoaded', () => {

    // ── Счётчик символов для textarea описания ──────────────────
    const textarea = document.querySelector('textarea[name="Description"]');
    const hint     = textarea?.parentElement?.querySelector('.form-hint');
    if (textarea && hint) {
        const max = parseInt(textarea.getAttribute('maxlength') || '1000');
        const update = () => {
            const left = max - textarea.value.length;
            hint.textContent = `Осталось символов: ${left}`;
            hint.style.color = left < 50 ? '#c0392b' : '';
        };
        textarea.addEventListener('input', update);
        update();
    }

    // ── Авто-скрытие alert-сообщений через 4 сек ────────────────
    document.querySelectorAll('.alert').forEach(el => {
        setTimeout(() => {
            el.style.transition = 'opacity .5s';
            el.style.opacity = '0';
            setTimeout(() => el.remove(), 500);
        }, 4000);
    });

    // ── Подсветка активного пункта навигации ────────────────────
    const path = location.pathname.toLowerCase();
    document.querySelectorAll('.nav-link').forEach(link => {
        const href = link.getAttribute('href')?.toLowerCase();
        if (href && path === href) link.classList.add('nav-link--active');
    });
});
