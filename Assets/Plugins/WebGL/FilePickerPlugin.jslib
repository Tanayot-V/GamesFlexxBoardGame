mergeInto(LibraryManager.library, {
  InitializeVisibleFileInput: function () {
    if (!window.fileInputCreated) {
      const input = document.createElement('input');
      input.type = 'file';
      input.accept = 'image/png, image/jpeg';
      input.style.position = 'absolute';
      input.style.top = '0';
      input.style.left = '0';
      input.style.transform = '';

      input.style.width = '200px';         // ✅ กำหนดความกว้าง
      input.style.height = '50px';         // ✅ กำหนดความสูง
      input.style.padding = '10px 20px';   // เพิ่มพื้นที่ภายในปุ่ม (optional)
      input.style.fontSize = '16px';       // ขนาดตัวอักษร (optional)
      input.style.border = '2px solidrgb(0, 0, 0)';
      input.style.borderRadius = '8px';    // มุมโค้งเล็กน้อย (optional)
      input.style.zIndex = '9999';
      input.style.display = 'block';       // แสดงปุ่ม
      input.style.backgroundColor = '#9B563D'; // สีพื้นหลัง (optional)
      
      input.onchange = function(event) {
        let file = event.target.files[0];
        if (file) {
          let reader = new FileReader();
          reader.onload = function(e) {
            let base64String = e.target.result.split(',')[1];
            unityInstance.SendMessage("WebGLFileLoaderButton", "OnFileSelected", base64String);
          };
          reader.readAsDataURL(file);
        }
      };
      
      document.body.appendChild(input);
      window.fileInputCreated = true;
    }
  },
  
  // สร้างฟังก์ชันเพื่อแสดง/ซ่อน input
  ShowFileInput: function() {
    const input = document.querySelector('input[type="file"]');
    if (input) {
      input.style.display = 'block';
    }
  },
  
  HideFileInput: function() {
    const input = document.querySelector('input[type="file"]');
    if (input) {
      input.style.display = 'none';
    }
  }
});