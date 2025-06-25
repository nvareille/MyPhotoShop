var Module = 
{
    Canvas: null,
    Ctx: null,
    ImagePtr: null,
    ImageData: null,
    Painting: false,
    onRuntimeInitialized: () => {
        console.log("Chargé ma GL");
        _SetDebug(false);
    
        Canvas = document.getElementById("canvas");
        Ctx = Canvas.getContext("2d");

        document.getElementById("LoadFile").addEventListener("change", Module.LoadFile);
        document.getElementById("grayscale").addEventListener("click", Module.Grayscale);
        document.getElementById("colorPicker").addEventListener("change", Module.ChangeBrushColor);
        Canvas.addEventListener("mousedown", Module.MouseDown);
        Canvas.addEventListener("mouseup", Module.MouseUp);
        Canvas.addEventListener("mousemove", Module.MoveMouse);
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
    ChangeBrushColor(e)
    {
        let value = e.target.value.substring(1, 7);
        let r = parseInt("0x" + value.substring(0, 2), 16);
        let g = parseInt("0x" + value.substring(2, 4), 16);
        let b = parseInt("0x" + value.substring(4, 6), 16);
        
        Module._ChangeBrushColor(r, g, b);
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

        Module.ImageData = Ctx.getImageData(0, 0, Canvas.width, Canvas.height);
        let ptr = _malloc(Canvas.width * Canvas.height * 4);

        HEAPU8.set(Module.ImageData.data, ptr);

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

        let data = HEAPU8.subarray(Module.ImagePtr, Module.ImagePtr + Canvas.width * Canvas.height * 4);
        
        Module.ImageData.data.set(data);
        Ctx.putImageData(Module.ImageData, 0, 0);
    },
    MousePosition(e)
    {
        let rect = Canvas.getBoundingClientRect();
        let x = Math.round(e.clientX - rect.left);
        let y = Math.round(e.clientY - rect.top);

        return ({x, y});
    },
    MouseDown(e)
    {
        let pos = Module.MousePosition(e);
            
        Module.Painting = true;
    },
    MouseUp(e)
    {
        Module.Painting = false;
    },
    MoveMouse(e)
    {
        let pos = Module.MousePosition(e);

        if (Module.Painting)
        {
            Module.ApplyPaint(pos)
        }
    },
    ApplyPaint(pos)
    {
        Module._ApplyPaint(pos.x, pos.y);
        Module.DisplayImage();
    }
};

