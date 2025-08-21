// cloudinaryUpload.js
console.log("Cloudinary Upload JS loaded");

/**
 * Upload 1 file lên Cloudinary và trả về URL
 * @param {File} file - File muốn upload
 * @returns {Promise<string>} - URL ảnh trả về
 */
async function uploadToCloudinary(file) {
  if (!file) throw new Error("File không hợp lệ");

  const formData = new FormData();
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
    return data.secure_url; // Trả về URL thực
  } catch (err) {
    console.error("Upload Cloudinary failed", err);
    throw err;
  }
}
