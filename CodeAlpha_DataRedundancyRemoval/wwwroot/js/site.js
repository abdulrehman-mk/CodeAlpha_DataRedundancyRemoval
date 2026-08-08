// Site-wide JavaScript for CodeAlpha_DataRedundancyRemoval
document.addEventListener('DOMContentLoaded', function () {
    // Auto-fill unit price when a product is selected on the Order Detail create form.
    var productSelect = document.getElementById('ProductId');
    var unitPriceInput = document.getElementById('UnitPrice');

    if (productSelect && unitPriceInput && productSelect.dataset.prices) {
        var prices = JSON.parse(productSelect.dataset.prices);
        productSelect.addEventListener('change', function () {
            if (prices[this.value] !== undefined) {
                unitPriceInput.value = prices[this.value];
            }
        });
    }
});
