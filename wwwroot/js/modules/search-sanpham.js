// search-sanpham.js

export function initSearchSanPham({
    inputSelector = '#search-input',
    dropdownSelector = '#search-dropdown',
    apiUrl = '/Admin/BanHangTaiQuay/SearchSanPham',
    onProductSelected = () => {}
}) {
    const $input = $(inputSelector);
    const $dropdown = $(dropdownSelector);

    $input.on('input', function () {
        const keyword = $(this).val().trim();
        if (keyword.length < 1) {
            $dropdown.removeClass('show').empty();
            return;
        }

        $.get(apiUrl, { keyword }, function (products) {
            $dropdown.empty();
            if (products.length === 0) {
                $dropdown.removeClass('show');
                return;
            }
            products.forEach(product => {
                const item = `<div class="dropdown-item" data-id="${product.id}" data-name="${product.name}">${product.name}</div>`;
                $dropdown.append(item);
            });
            $dropdown.addClass('show');
        });
    });

    $dropdown.on('click', '.dropdown-item', function () {
        const product = {
            id: $(this).data('shoeDetailID'),
            name: $(this).data('tenSp')
        };
        $input.val(product.name);
        $dropdown.removeClass('show').empty();
        onProductSelected(product);
    });

    $(document).on('click', function (e) {
        if (!$(e.target).closest(inputSelector + ',' + dropdownSelector).length) {
            $dropdown.removeClass('show').empty();
        }
    });
}
