(() => {
    const canvas = document.getElementById('builder-canvas');
    const preview = document.getElementById('preview-area');
    const addBtn = document.getElementById('add-field');
    const saveDraft = document.getElementById('save-draft');
    const publishBtn = document.getElementById('publish');

    let fields = [];

    function renderCanvas() {
        canvas.innerHTML = '';
        fields.sort((a,b) => a.sortOrder - b.sortOrder).forEach(f => {
            const el = document.createElement('div');
            el.className = 'mb-2 p-2 border bg-white';
            el.dataset.id = f.tempId;
            el.innerHTML = `
                <div class="d-flex justify-content-between align-items-center">
                    <div><strong>${escapeHtml(f.label||'Untitled')}</strong> <small>(${f.fieldType})</small></div>
                    <div>
                        <button class="btn btn-sm btn-secondary edit-field">Edit</button>
                        <button class="btn btn-sm btn-danger del-field">Delete</button>
                    </div>
                </div>`;
            canvas.appendChild(el);

            el.querySelector('.edit-field').addEventListener('click', ()=> openEditor(f.tempId));
            el.querySelector('.del-field').addEventListener('click', ()=> { fields = fields.filter(x=>x.tempId!==f.tempId); renderCanvas(); renderPreview(); });
        });
    }

    function renderPreview(){
        preview.innerHTML = '';
        fields.sort((a,b) => a.sortOrder - b.sortOrder).forEach(f => {
            const row = document.createElement('div');
            row.className = 'mb-2';
            let inputHtml = '';
            switch(f.fieldType){
                case 'Dropdown':
                    inputHtml = `<select class="form-select">${(f.options||[]).map(o=>`<option value="${escapeHtml(o.value)}">${escapeHtml(o.display||o.value)}</option>`).join('')}</select>`;
                    break;
                case 'MultiSelect':
                    inputHtml = `${(f.options||[]).map(o=>`<div class="form-check"><input class="form-check-input" type="checkbox"/><label class="form-check-label">${escapeHtml(o.display||o.value)}</label></div>`).join('')}`;
                    break;
                default:
                    inputHtml = `<input class="form-control" placeholder="${escapeHtml(f.label||'')}" />`;
            }
            row.innerHTML = `<label>${escapeHtml(f.label||'')}</label>${inputHtml}`;
            preview.appendChild(row);
        });
    }

    function openEditor(tempId){
        const f = fields.find(x=>x.tempId===tempId);
        if(!f) return;
        const label = prompt('Label', f.label||'');
        if(label===null) return;
        f.label = label;
        const type = prompt('Field Type (Text, Number, Date, Dropdown, Checkbox, File, Email, Phone, MultiSelect, Image, Audio, Video)', f.fieldType||'Text');
        if(type) f.fieldType = type;
        const req = confirm('Required?');
        f.isRequired = req;
        renderCanvas(); renderPreview();
    }

    addBtn.addEventListener('click', ()=>{
        const id = 't'+Math.random().toString(36).substr(2,9);
        fields.push({ tempId: id, label: 'New Field', fieldType: 'Text', isRequired:false, sortOrder: fields.length+1, options: [] });
        renderCanvas(); renderPreview();
    });

    saveDraft.addEventListener('click', async ()=>{
        const name = document.getElementById('form-name').value;
        if(!name){ alert('Provide form name'); return; }
        const dto = { Name: name, Fields: fields.map((f,i)=>({ Label:f.label, FieldType: f.fieldType, IsRequired: f.isRequired, SortOrder: f.sortOrder, MetaJson: null })) };
        const res = await fetch('/Admin/Forms/Create', { method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify(dto)});
        if(res.ok) { alert('Draft saved'); } else { alert('Save failed'); }
    });

    publishBtn.addEventListener('click', ()=>{
        var modal = new bootstrap.Modal(document.getElementById('publishModal'));
        modal.show();
    });

    document.getElementById('confirm-publish').addEventListener('click', async ()=>{
        const name = document.getElementById('form-name').value;
        if(!name){ alert('Provide form name'); return; }
        const deadlineInput = document.getElementById('deadline').value;
        if(!deadlineInput){ alert('Provide deadline'); return; }
        const deadline = new Date(deadlineInput).toISOString();
        const dto = { Name: name, Fields: fields.map((f,i)=>({ Label:f.label, FieldType: f.fieldType, IsRequired: f.isRequired, SortOrder: f.sortOrder, MetaJson: null })) };
        const create = await fetch('/Admin/Forms/Create', { method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify(dto)});
        if(!create.ok){ alert('Create failed'); return; }
        const created = await create.json();
        const formId = created.formId || created.FormId || created.id || created.formId;
        const pub = await fetch(`/Admin/Forms/Publish?id=${formId}`, { method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify(deadline) });
        if(pub.ok) { alert('Published'); location.href = '/'; } else { alert('Publish failed'); }
    });

    function escapeHtml(s){ if(!s) return ''; return s.replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;').replace(/"/g,'&quot;'); }

    renderCanvas(); renderPreview();
})();