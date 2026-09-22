window.initResizablePanes = (containerId, dividerId, leftId, rightId, minLeftPercent = 30, maxLeftPercent = 80, initialLeftPercent = 75) => {
    const container = document.getElementById(containerId);
    const divider = document.getElementById(dividerId);
    const left = document.getElementById(leftId);
    const right = document.getElementById(rightId);

    if (!container || !divider || !left || !right) return;

    function applySplit(leftPercent) {
        leftPercent = Math.max(minLeftPercent, Math.min(maxLeftPercent, leftPercent));
        left.style.flexBasis = `${leftPercent}%`;
        right.style.flexBasis = `${100 - leftPercent}%`;
    }

    applySplit(initialLeftPercent);

    let isDragging = false;

    divider.addEventListener('mousedown', (e) => {
        isDragging = true;
        document.body.style.cursor = 'col-resize';
        e.preventDefault();
    });

    document.addEventListener('mousemove', (e) => {
        if (!isDragging) return;

        const containerRect = container.getBoundingClientRect();
        const dividerRect = divider.getBoundingClientRect();
        const containerStyle = window.getComputedStyle(container);

        const rawGap = containerStyle.columnGap || containerStyle.gap;
        let gap = parseFloat(rawGap);
        if (Number.isNaN(gap)) {
            gap = 0; // fall back to 0 rather than corrupting every subsequent calculation
        }

        const usableWidth = containerRect.width - dividerRect.width - (gap * 2);
        const leftPixels = e.clientX - containerRect.left - gap;
        const leftPercent = (leftPixels / usableWidth) * 100;

        applySplit(leftPercent);
    });

    document.addEventListener('mouseup', () => {
        isDragging = false;
        document.body.style.cursor = '';
    });
};