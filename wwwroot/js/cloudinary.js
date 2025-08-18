console.log("Js đang được load");
document.addEventListener("DOMContentLoaded", () => {
  const imageUpload = document.getElementById("imageUpload");
  const imageUrlInput = document.getElementById("ImageUrl");
  const preview = document.getElementById("previewImage");

  if (!imageUpload) return;

  imageUpload.addEventListener("change", async function () {
    const file = this.files[0];
    if (!file) return;

    let formData = new FormData();
    formData.append("file", file);
    formData.append("upload_preset", UPLOAD_PRESET);

    try {
      const res = await fetch(
        `https://api.cloudinary.com/v1_1/${CLOUD_NAME}/image/upload`,
        {
          method: "POST",
          body: formData,
        }
      );
      const data = await res.json();

      imageUrlInput.value = data.secure_url;
      preview.src = data.secure_url;
      preview.classList.remove("d-none");
    } catch (err) {
      alert("Upload ảnh thất bại!");
      console.error(err);
    }
  });
});
