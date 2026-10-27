(function () {
    const input = document.getElementById('menu-search');
    const grid = document.getElementById('menu-grid');
    if (!input || !grid) return;

    const cards = grid.querySelectorAll('.card');
    const noResults = document.getElementById('search-empty');

    input.addEventListener('input', function () {
        const term = input.value.trim().toLowerCase();
        let shown = 0;

        cards.forEach(function (card) {
            const match = card.dataset.search.includes(term);
            card.hidden = !match;
            if (match) shown++;
        });

        noResults.hidden = shown > 0;
    });
})();
