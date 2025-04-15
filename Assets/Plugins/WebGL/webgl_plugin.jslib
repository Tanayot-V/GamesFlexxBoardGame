mergeInto(LibraryManager.library, {
    DownloadScreenshot: function (base64ImagePtr) {
        var base64Image = UTF8ToString(base64ImagePtr);

        let byteCharacters = atob(base64Image);
        let byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        let byteArray = new Uint8Array(byteNumbers);
        let blob = new Blob([byteArray], { type: "image/png" });

        let link = document.createElement("a");
        link.href = URL.createObjectURL(blob);
        link.download = "screenshot.png";
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);

        console.log("✅ Screenshot Downloaded");
    },

    ShareText: function (textPtr) {
        var text = UTF8ToString(textPtr);
        console.log("📤 Sharing text: ", text);

        if (!navigator.share) {
            alert("❌ Web Share API ไม่รองรับบนอุปกรณ์นี้!");
            return;
        }

        navigator.share({
            title: "แชร์จากเกมของคุณ",
            text: text,
        })
        .then(() => console.log("✅ แชร์สำเร็จ!"))
        .catch(err => console.error("❌ แชร์ล้มเหลว:", err));
    },

    ShareImage: function (base64ImagePtr) {
        var base64Image = UTF8ToString(base64ImagePtr);
        console.log("📤 Sharing image...");

        if (!navigator.share) {
            alert("❌ Web Share API ไม่รองรับบนอุปกรณ์นี้!");
            return;
        }

        let byteCharacters = atob(base64Image.split(",")[1]);
        let byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        let byteArray = new Uint8Array(byteNumbers);
        let blob = new Blob([byteArray], { type: "image/png" });
        let file = new File([blob], "screenshot.png", { type: "image/png" });

        if (!navigator.canShare || !navigator.canShare({ files: [file] })) {
            alert("❌ ไม่รองรับการแชร์ไฟล์!");
            return;
        }

        navigator.share({
            title: "📷 แชร์ภาพจากเกม!",
            files: [file],
        })
        .then(() => console.log("✅ แชร์สำเร็จ!"))
        .catch(err => console.error("❌ แชร์ล้มเหลว:", err));
    },

    OpenFilePicker: function () {
        if (typeof unityInstance === "undefined" || unityInstance === null) {
            console.error("❌ Unity is not loaded yet!");
            return;
        }

        let input = document.createElement("input");
        input.type = "file";
        input.accept = "image/png, image/jpeg";

        input.onchange = function (event) {
            let file = event.target.files[0];
            if (file) {
                let reader = new FileReader();
                reader.onload = function (e) {
                    let base64String = e.target.result.split(',')[1]; 
                    unityInstance.SendMessage("WebGLFileLoader", "OnFileSelected", base64String);
                };
                reader.readAsDataURL(file);
            }
        };

        input.click();
        console.log("📤 Open File Picker Triggered");
    },

    ShareBase64Image: function (base64ImagePtr) {
        var base64Image = UTF8ToString(base64ImagePtr);
        console.log("📤 Preparing image for Facebook share...");

        let byteCharacters = atob(base64Image);
        let byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        let byteArray = new Uint8Array(byteNumbers);
        let blob = new Blob([byteArray], { type: "image/png" });

        let imageUrl = URL.createObjectURL(blob);
        let fbShareUrl = "https://www.facebook.com/sharer/sharer.php?u=" + encodeURIComponent(imageUrl);
        window.open(fbShareUrl, "_blank");

        setTimeout(() => {
            URL.revokeObjectURL(imageUrl);
            console.log("✅ Temporary URL Revoked");
        }, 10000);
    },
});