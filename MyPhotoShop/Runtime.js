var Module = 
{
    Canvas: null,
    Ctx: null,
    ImagePtr: null,
    onRuntimeInitialized: () => {
        console.log("Chargé ma GL");
        _SetDebug(true);
    
        Canvas = document.getElementById("canvas");
        Ctx = Canvas.getContext("2d");

        document.getElementById("LoadFile").addEventListener("change", Module.LoadFile);
        document.getElementById("grayscale").addEventListener("click", Module.Grayscale);
        Canvas.addEventListener("mousedown", Module.HandleMousePosition); 
    },
    LoadFile: async (e) =>
    {
        const img = new Image();
        img.src = URL.createObjectURL(e.target.files[0]);
        await img.decode();

        Canvas.width = img.width;
        Canvas.height = img.height;
        Ctx.drawImage(img, 0, 0);

        Module.AllocImageAndSet();
    },
    Grayscale: () =>
    {
        console.log("Noir & blanc");

        _Grayscale();
        Module.DisplayImage();
    },
    AllocImageAndSet: () =>
    {
        if (Module.ImagePtr != null)
            _Free(Module.ImagePtr);

        let buffer = Ctx.getImageData(0, 0, Canvas.width, Canvas.height);
        let ptr = _malloc(Canvas.width * Canvas.height * 4);

        HEAPU8.set(buffer.data, ptr);

        Module.ImagePtr = ptr;
        Module.SetImageData(ptr, Canvas.width, Canvas.height)
    },
    SetImageData: (ptr, x, y) =>
    {
        _LoadImage(ptr, x, y)
    },
    DisplayImage: () =>
    {
        if (Module.ImagePtr == null)
            return;

        let buffer = Ctx.getImageData(0, 0, Canvas.width, Canvas.height);
        let data = HEAPU8.subarray(Module.ImagePtr, Module.ImagePtr + Canvas.width * Canvas.height * 4);
        
        buffer.data.set(data);
        Ctx.putImageData(buffer, 0, 0);
    },
    HandleMousePosition: (e) =>
    {
        let rect = canvas.getBoundingClientRect();
        let x = e.clientX - rect.left;
        let y = e.clientY - rect.top;
            
        console.log(`${x} ${y}`);
    }
};

