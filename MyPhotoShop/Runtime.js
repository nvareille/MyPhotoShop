var Module = 
{
        onRuntimeInitialized: () => {
            console.log("Chargé ma GL");
        
            document.getElementById("LoadFile").addEventListener("change", Module.LoadFile);
            document.getElementById("grayscale").addEventListener("click", Module.Grayscale);
        },
        LoadFile: async (e) =>
        {
            const canvas = document.getElementById("canvas")
            const ctx = canvas.getContext("2d");
            const img = new Image();
            img.src = URL.createObjectURL(e.target.files[0]);
            await img.decode();

            canvas.width = img.width;
            canvas.height = img.height;
            ctx.drawImage(img, 0, 0);
        },
        Grayscale: () =>
        {
            console.log("Noir & blanc");

            const canvas = document.getElementById("canvas")
            const ctx = canvas.getContext("2d");

            let buffer = ctx.getImageData(0, 0, canvas.width, canvas.height);
            let ptr = _malloc(canvas.width * canvas.height * 4);

            HEAPU8.set(buffer.data, ptr);

            _Grayscale(ptr, canvas.width * canvas.height * 4);

            let data = HEAPU8.subarray(ptr, ptr + canvas.width * canvas.height * 4);
            
            buffer.data.set(data);
            ctx.putImageData(buffer, 0, 0);
        }
};

