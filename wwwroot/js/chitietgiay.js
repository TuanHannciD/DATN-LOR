

document.addEventListener('DOMContentLoaded', () => {
  const editModal = new bootstrap.Modal(document.getElementById('editModal'));
  const editForm = document.getElementById('editForm');
  
  //fillSelectOptions 
  // Điền các option vào select box
  // Dùng để điền các option vào select box khi lấy dữ liệu chi tiết giày
  function fillSelectOptions(selectId, options, selectValue) {
    if (!options) {
      console.error(`Danh sách options cho ${selectId} bị null hoặc undefined.`);
      return;
    }
    const select = document.getElementById(selectId);
    select.innerHTML = '';
    options.forEach(option => {
      const opt = document.createElement('option');
      opt.value = option.id;
      opt.text = option.name;
      if (option.id === selectValue) {
        opt.selected = true;
      }
      select.appendChild(opt);
    });
  }
  //fillmodalInputs
  // Điền các input trong modal
  // Dùng để điền các input trong modal khi lấy dữ liệu chi tiết giày
  function fillModalInputs(data) {
    document.getElementById('ShoeDetailID').value = data.shoeDetailID;
    document.getElementById('SoLuong').value = data.soLuong;
    document.getElementById('Gia').value = data.gia;
  }

  document.querySelectorAll('a.btn-warning').forEach(btn => {
    btn.addEventListener('click', async e => {
      e.preventDefault();
      const url = new URL(btn.href, window.location.origin);
      const id = url.pathname.split('/').pop();

      try {
        const res = await fetch(`/Admin/ChiTietGiay/GetById/${id}`);
        if (!res.ok) {
          showToast('error', 'Lỗi', 'Không thể lấy dữ liệu chi tiết giày.');
          return;
        }
        const data = await res.json();

        const selectRes = await fetch('/Admin/ChiTietGiay/GetSelectLists');
        if (!selectRes.ok) {
          alert('Không lấy được danh sách lựa chọn');
          return;
        }
        const selectData = await selectRes.json();

        // Lấy danh sách từ ViewBag 

        // Fill các select box trước vì có chọn option rồi
        fillSelectOptions('ShoeID', selectData.shoeList, data.shoeID);
        fillSelectOptions('SizeID', selectData.sizeList, data.sizeID);
        fillSelectOptions('ColorID', selectData.colorList, data.colorID);
        fillSelectOptions('MaterialID', selectData.materialList, data.materialID);
        fillSelectOptions('BrandID', selectData.brandList, data.brandID);
        fillSelectOptions('CategoryID', selectData.categoryList, data.categoryID);


        // Fill các input còn lại
        fillModalInputs(data);

        // Hiển thị modal
        editModal.show();

      } catch (err) {
        console.error(err);
        alert('Lỗi khi lấy dữ liệu chi tiết.');
      }
    });
  });

  editForm.addEventListener('submit', async e => {
    e.preventDefault();
     // ✅ Gọi hàm validate trước
    if (!validateForm(editForm)) {
        // Nếu validate fail → không gửi API
        return;
    }
    const entity = {
      ShoeDetailID: document.getElementById('ShoeDetailID').value,
      ShoeID: document.getElementById('ShoeID').value,
      SizeID: document.getElementById('SizeID').value,
      ColorID: document.getElementById('ColorID').value,
      MaterialID: document.getElementById('MaterialID').value,
      BrandID: document.getElementById('BrandID').value,
      CategoryID: document.getElementById('CategoryID').value,
      SoLuong: parseInt(document.getElementById('SoLuong').value),
      Gia: parseFloat(document.getElementById('Gia').value)
    };

    // Kiểm tra dữ liệu trước khi gửi
    console.log('Dữ liệu gửi đi:', entity);
    
    try {
      const res = await fetch('/Admin/ChiTietGiay/Update', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(entity)
      });
      const data = await res.json()
      console.log('Kết quả trả về:', data);
      if (!res.ok) {

        showToast('error', data.message);
        return;
      }

      showToast(data.message, 'Thành công');
      editModal.hide();
      //thêm delay reload lại trang
      setTimeout(() => {
        window.location.reload();
      }, 2000);


    } catch (error) {
      showToast('error', 'Lỗi', 'Không thể cập nhật chi tiết giày.');
      console.error(error);
    }
  });
});
