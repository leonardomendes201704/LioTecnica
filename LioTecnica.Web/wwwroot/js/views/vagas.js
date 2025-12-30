// ========= Logo (embutido em Data URI - auto contido)
    // ObservaÃ§Ã£o: o arquivo fornecido veio como WebP (mesmo com nome .png).
    const seed = window.__seedData || {};
    const LOGO_DATA_URI = "data:image/webp;base64,UklGRngUAABXRUJQVlA4IGwUAAAQYwCdASpbAVsBPlEokUajoqGhIpNoyHAK7AQYJjYQmG9Dtu/6p6QZ4lQd6lPde+Jk3i3kG2EoP+QW0c0h8Oe3jW2C5zE0o9jzZ1x2fX9cZlX0d7rW8r0vQ9p3d2nJ1bqzQfQZxVwTt7mJvU8j1GqF4oJc8Qb+gq+oQyHcQyYc2b9u2fYf0Rj9x9hRZp2Y2xK0yVQ8Hj4p6w8B1K2cKk2mY9m2r8kz3a4m7xG4xg9m5VjzP3E4RjQH8fYkC4mB8g0vR3c5h1D0yE8Qzv7t7gQj0Z9yKk3cWZgVnq3l1kq6rE8oWc4z6oZk8k0b1o9m8p2m+QJ3nJm6GgA=";
function enumFirstCode(key, fallback){
      const list = getEnumOptions(key);
      return list.length ? list[0].code : fallback;
    }

    const AREA_ALL = enumFirstCode("vagaAreaFilter", "all");
    const DEFAULT_MODALIDADE = enumFirstCode("vagaModalidade", "Presencial");
    const DEFAULT_STATUS = enumFirstCode("vagaStatus", "aberta");
    const DEFAULT_SENIORIDADE = enumFirstCode("vagaSenioridade", "Junior");
    const DEFAULT_CATEGORIA = enumFirstCode("requisitoCategoria", "Competencia");
    const EMPTY_TEXT = "—";
    const BULLET = "•";

    function setText(root, role, value, fallback = EMPTY_TEXT){
      if(!root) return;
      const el = root.querySelector(`[data-role="${role}"]`);
      if(!el) return;
      el.textContent = (value ?? fallback);
    }
function fmtStatus(s){
      const map = { aberta:"Aberta", pausada:"Pausada", fechada:"Fechada" };
      return map[s] || s;
    }

    function buildStatusBadge(s){
      const map = {
        aberta:  "success",
        pausada: "warning",
        fechada: "secondary"
      };
      const bs = map[s] || "primary";
      const badge = cloneTemplate("tpl-vaga-status-badge");
      if(!badge) return document.createElement("span");
      badge.classList.add("text-bg-" + bs);
      const text = badge.querySelector('[data-role="text"]');
      if(text) text.textContent = fmtStatus(s);
      return badge;
    }
// ========= Storage
    const STORE_KEY = "lt_rh_vagas_v1";

    const state = {
      vagas: [],
      selectedId: null,
      filters: { q:"", status:"all", area:"all" }
    };

    function loadState(){
      try{
        const raw = localStorage.getItem(STORE_KEY);
        if(!raw) return false;
        const data = JSON.parse(raw);
        if(!data || !Array.isArray(data.vagas)) return false;
        state.vagas = data.vagas;
        state.selectedId = data.selectedId ?? null;
        return true;
      }catch{
        return false;
      }
    }

    function saveState(){
      localStorage.setItem(STORE_KEY, JSON.stringify({
        vagas: state.vagas,
        selectedId: state.selectedId
      }));
    }

    function seedIfEmpty(){
      if(state.vagas.length) return;

      const vagasSeed = Array.isArray(seed.vagas) ? seed.vagas : [];
      if(!vagasSeed.length) return;

      state.vagas = vagasSeed;
      state.selectedId = seed.selectedVagaId || vagasSeed[0]?.id || null;
      saveState();
    }

    // ========= Rendering
    function updateKpis(){
      const total = state.vagas.length;
      const abertas = state.vagas.filter(v => v.status === "aberta").length;
      const pausadas = state.vagas.filter(v => v.status === "pausada").length;
      const fechadas = state.vagas.filter(v => v.status === "fechada").length;

      $("#kpiTotal").textContent = total;
      $("#kpiAbertas").textContent = abertas;
      $("#kpiPausadas").textContent = pausadas;
      $("#kpiFechadas").textContent = fechadas;
    }

    function distinctAreas(){
      const set = new Set(state.vagas.map(v => v.area).filter(Boolean));
      return Array.from(set).sort((a,b)=>a.localeCompare(b,"pt-BR"));
    }

    function renderAreaFilter(){
      const areas = distinctAreas();
      const sel = $("#fArea");
      const cur = sel.value || AREA_ALL;
      sel.replaceChildren();
      getEnumOptions("vagaAreaFilter").forEach(opt => {
        sel.appendChild(buildOption(opt.code, opt.text, opt.code === cur));
      });
      areas.forEach(a => {
        sel.appendChild(buildOption(a, a, a === cur));
      });
      sel.value = areas.includes(cur) ? cur : AREA_ALL;
    }

    function getFilteredVagas(){
      const q = (state.filters.q || "").trim().toLowerCase();
      const st = state.filters.status;
      const ar = state.filters.area;

      return state.vagas.filter(v => {
        if(st !== "all" && v.status !== st) return false;
        if(ar !== "all" && (v.area || "") !== ar) return false;

        if(!q) return true;

        const blob = [
          v.codigo, v.titulo, v.area, v.modalidade, v.cidade, v.uf, v.senioridade
        ].join(" ").toLowerCase();

        return blob.includes(q);
      });
    }

    function renderList(){
      const tbody = $("#tblVagas");
      tbody.replaceChildren();

      const rows = getFilteredVagas();
      if(!rows.length){
        const emptyRow = cloneTemplate("tpl-vaga-empty-row");
        if(emptyRow) tbody.appendChild(emptyRow);
        return;
      }

      rows.forEach(v => {
        const reqTotal = (v.requisitos || []).length;
        const reqObrig = (v.requisitos || []).filter(r => !!r.obrigatorio).length;
        const isSel = v.id === state.selectedId;

        const tr = cloneTemplate("tpl-vaga-row");
        if(!tr) return;
        tr.style.cursor = "default";
        if(isSel) tr.classList.add("table-active");

        setText(tr, "vaga-title", v.titulo);
        setText(tr, "vaga-code", v.codigo);
        setText(tr, "vaga-modalidade", v.modalidade);
        setText(tr, "vaga-area", v.area);
        setText(tr, "req-total", `${reqTotal} total`);
        setText(tr, "req-obrig", `${reqObrig} obrig.`);

        const thrVal = clamp(parseInt(v.threshold ?? 0,10)||0,0,100);
        setText(tr, "vaga-thr", `${thrVal}%`);

        const locParts = [v.cidade, v.uf].filter(Boolean);
        const hasLoc = locParts.length > 0;
        setText(tr, "vaga-location", hasLoc ? locParts.join(" - ") : EMPTY_TEXT);
        toggleRole(tr, "vaga-location", hasLoc);
        toggleRole(tr, "vaga-location-sep", hasLoc);

        const statusHost = tr.querySelector('[data-role="status-host"]');
        if(statusHost) statusHost.replaceChildren(buildStatusBadge(v.status));

        tr.querySelectorAll("button[data-act]").forEach(btn => {
          btn.dataset.id = v.id;
        });

        tr.addEventListener("click", (ev) => {
          const btn = ev.target.closest("button[data-act]");
          if(btn){
            ev.preventDefault();
            ev.stopPropagation();
            const act = btn.dataset.act;
            const id = btn.dataset.id;
            if(act === "detail") openDetailModal(id);
            if(act === "edit") openVagaModal("edit", id);
            if(act === "dup") duplicateVaga(id);
            if(act === "del") deleteVaga(id);
            return;
          }
        });

        tbody.appendChild(tr);
      });
    }

    function findVaga(id){
      return state.vagas.find(v => v.id === id) || null;
    }

    function selectVaga(id){
      state.selectedId = id;
      saveState();
      renderList();
      renderDetail();
    }

    function openDetailModal(id){
      selectVaga(id);
      const modal = bootstrap.Modal.getOrCreateInstance($("#modalVagaDetalhes"));
      modal.show();
    }

    // ========= Detail UI
    function renderDetail(){
      const host = $("#detailHost");
      host.replaceChildren();

      const v = findVaga(state.selectedId);
      if(!v){
        const empty = cloneTemplate("tpl-vaga-detail-empty");
        if(empty) host.appendChild(empty);
        return;
      }

      const reqTotal = (v.requisitos || []).length;
      const reqObrig = (v.requisitos || []).filter(r => !!r.obrigatorio).length;

      const updated = v.updatedAt ? new Date(v.updatedAt) : null;
      const updatedTxt = updated ? updated.toLocaleString("pt-BR", { day:"2-digit", month:"2-digit", year:"numeric", hour:"2-digit", minute:"2-digit" }) : EMPTY_TEXT;

      const weights = v.weights || { competencia:40, experiencia:30, formacao:15, localidade:15 };
      const sumW = (weights.competencia||0) + (weights.experiencia||0) + (weights.formacao||0) + (weights.localidade||0);

      const thrVal = clamp(parseInt(v.threshold ?? 0,10)||0,0,100);

      const root = cloneTemplate("tpl-vaga-detail");
      if(!root) return;

      setText(root, "detail-title", v.titulo);
      setText(root, "detail-code", v.codigo);
      setText(root, "detail-area", v.area);
      setText(root, "detail-modalidade", v.modalidade);

      const statusHost = root.querySelector('[data-role="detail-status-host"]');
      if(statusHost) statusHost.replaceChildren(buildStatusBadge(v.status));

      setText(root, "detail-updated", updatedTxt);
      setText(root, "detail-req-total", `${reqTotal} requisitos`);
      setText(root, "detail-req-obrig", `${reqObrig} obrigatorios`);
      setText(root, "detail-thr", `${thrVal}%`);

      setText(root, "detail-descricao", v.descricao);
      const locParts = [v.cidade, v.uf].filter(Boolean);
      setText(root, "detail-local", locParts.join(" - ") || EMPTY_TEXT);
      setText(root, "detail-senioridade", v.senioridade);

      const thresholdRange = root.querySelector("#thresholdRange");
      const thresholdLabel = root.querySelector("#thresholdLabel");
      if(thresholdRange) thresholdRange.value = thrVal;
      if(thresholdLabel) thresholdLabel.textContent = `${thrVal}%`;

      const weightsSum = root.querySelector("#weightsSum");
      if(weightsSum) weightsSum.textContent = sumW;

      const weightKeys = ["competencia","experiencia","formacao","localidade"];
      weightKeys.forEach(key => {
        const slider = root.querySelector("#w_" + key);
        const val = root.querySelector("#w_" + key + "_val");
        const w = clamp(parseInt(weights[key] ?? 0,10)||0, 0, 100);
        if(slider) slider.value = w;
        if(val) val.textContent = w;
      });

      host.appendChild(root);

      // bind detail actions + render req table + bind sliders
      bindDetailActions(v);
      renderReqTable(v);
    }

    function bindDetailActions(v){
      if(!v) return;

      // detail buttons
      $$("#detailHost [data-dact]").forEach(btn => {
        btn.addEventListener("click", () => {
          const act = btn.dataset.dact;
          if(act === "editvaga") openVagaModal("edit", v.id);
          if(act === "duplicate") duplicateVaga(v.id);
          if(act === "delete") deleteVaga(v.id);
          if(act === "addreq") openReqModal("new", v.id);
          if(act === "saveWeights") saveWeightsFromDetail(v.id);
          if(act === "simulate") simulateMatch(v.id);
          if(act === "simClear") clearSimulation();
        });
      });

      // threshold
      const r = $("#detailHost #thresholdRange");
      const lbl = $("#detailHost #thresholdLabel");
      if(r && lbl){
        r.addEventListener("input", () => {
          lbl.textContent = clamp(parseInt(r.value,10)||0,0,100) + "%";
        });
      }

      // weights
      ["competencia","experiencia","formacao","localidade"].forEach(k => {
        const slider = $("#detailHost #w_" + k);
        const val = $("#detailHost #w_" + k + "_val");
        if(slider && val){
          slider.addEventListener("input", () => {
            val.textContent = slider.value;
            updateWeightsSumInDetail();
          });
        }
      });

      // req search
      const reqSearch = $("#detailHost #reqSearch");
      if(reqSearch){
        reqSearch.addEventListener("input", () => renderReqTable(v));
      }
    }

    function updateWeightsSumInDetail(){
      const keys = ["competencia","experiencia","formacao","localidade"];
      let sum = 0;
      keys.forEach(k => {
        const slider = $("#detailHost #w_" + k);
        sum += (slider ? parseInt(slider.value,10) : 0) || 0;
      });
      const sumEl = $("#detailHost #weightsSum");
      if(sumEl) sumEl.textContent = sum;
    }

    function renderReqTable(v){
      const tbody = $("#detailHost #tblReq");
      if(!tbody) return;

      tbody.replaceChildren();
      const reqs = (v?.requisitos || []);

      const q = ($("#detailHost #reqSearch")?.value || "").trim().toLowerCase();
      const filtered = !q ? reqs : reqs.filter(r => {
        const blob = [r.categoria, r.termo, (r.sinonimos||[]).join(" "), r.obs].join(" ").toLowerCase();
        return blob.includes(q);
      });

      if(!filtered.length){
        const emptyRow = cloneTemplate("tpl-vaga-req-empty-row");
        if(emptyRow) tbody.appendChild(emptyRow);
        return;
      }

      filtered.forEach(r => {
        const tr = cloneTemplate("tpl-vaga-req-row");
        if(!tr) return;

        const toggleInput = tr.querySelector('[data-role="req-toggle"]');
        if(toggleInput){
          toggleInput.checked = !!r.obrigatorio;
          toggleInput.dataset.rid = r.id;
        }

        setText(tr, "req-categoria", r.categoria);
        setText(tr, "req-termo", r.termo);
        setText(tr, "req-obs", r.obs || EMPTY_TEXT);
        setText(tr, "req-peso", clamp(parseInt(r.peso ?? 0,10)||0,0,10));
        const syn = (r.sinonimos || []).join(", ");
        setText(tr, "req-sinonimos", syn || EMPTY_TEXT);

        tr.querySelectorAll("[data-ract]").forEach(el => {
          el.dataset.rid = r.id;
          el.addEventListener("click", (ev) => {
            ev.preventDefault();
            ev.stopPropagation();
            const act = el.dataset.ract;
            const rid = el.dataset.rid;
            if(act === "toggle") toggleReqMandatory(v.id, rid);
            if(act === "edit") openReqModal("edit", v.id, rid);
            if(act === "del") deleteReq(v.id, rid);
          });
        });

        tbody.appendChild(tr);
      });
    }

    
    // ========= CRUD: Vagas
    function openVagaModal(mode, id){
      const modal = bootstrap.Modal.getOrCreateInstance($("#modalVaga"));
      const isEdit = mode === "edit";
      $("#modalVagaTitle").textContent = isEdit ? "Editar vaga" : "Nova vaga";

      if(isEdit){
        const v = findVaga(id);
        if(!v) return;
        $("#vagaId").value = v.id;
        $("#vagaCodigo").value = v.codigo || "";
        $("#vagaTitulo").value = v.titulo || "";
        $("#vagaArea").value = v.area || "";
        $("#vagaModalidade").value = v.modalidade || DEFAULT_MODALIDADE;
        $("#vagaStatus").value = v.status || DEFAULT_STATUS;
        $("#vagaCidade").value = v.cidade || "";
        $("#vagaUF").value = v.uf || "";
        $("#vagaSenioridade").value = v.senioridade || DEFAULT_SENIORIDADE;
        $("#vagaThreshold").value = clamp(parseInt(v.threshold ?? 70,10)||70, 0, 100);
        $("#vagaDescricao").value = v.descricao || "";
      }else{
        $("#vagaId").value = "";
        $("#vagaCodigo").value = "";
        $("#vagaTitulo").value = "";
        $("#vagaArea").value = "";
        $("#vagaModalidade").value = DEFAULT_MODALIDADE;
        $("#vagaStatus").value = DEFAULT_STATUS;
        $("#vagaCidade").value = "";
        $("#vagaUF").value = "SP";
        $("#vagaSenioridade").value = DEFAULT_SENIORIDADE;
        $("#vagaThreshold").value = 70;
        $("#vagaDescricao").value = "";
      }

      modal.show();
    }

    function upsertVagaFromModal(){
      const id = $("#vagaId").value || null;
      const codigo = ($("#vagaCodigo").value || "").trim();
      const titulo = ($("#vagaTitulo").value || "").trim();
      const area = ($("#vagaArea").value || "").trim();
      const modalidade = ($("#vagaModalidade").value || "").trim();
      const status = ($("#vagaStatus").value || "").trim();
      const cidade = ($("#vagaCidade").value || "").trim();
      const uf = ($("#vagaUF").value || "").trim().toUpperCase().slice(0,2);
      const senioridade = ($("#vagaSenioridade").value || "").trim();
      const threshold = clamp(parseInt($("#vagaThreshold").value,10)||70, 0, 100);
      const descricao = ($("#vagaDescricao").value || "").trim();

      // validaÃ§Ã£o mÃ­nima
      if(!titulo){
        toast("Informe o tÃ­tulo da vaga.");
        return;
      }
      if(!area){
        toast("Selecione a Ã¡rea da vaga.");
        return;
      }
      if(!status){
        toast("Selecione o status da vaga.");
        return;
      }

      const now = new Date().toISOString();

      if(id){
        const v = findVaga(id);
        if(!v) return;

        v.codigo = codigo;
        v.titulo = titulo;
        v.area = area;
        v.modalidade = modalidade;
        v.status = status;
        v.cidade = cidade;
        v.uf = uf;
        v.senioridade = senioridade;
        v.threshold = threshold;
        v.descricao = descricao;
        v.updatedAt = now;

        toast("Vaga atualizada.");
        state.selectedId = v.id;
      }else{
        const v = {
          id: uid(),
          codigo,
          titulo,
          area,
          modalidade,
          status,
          cidade,
          uf,
          senioridade,
          threshold,
          descricao,
          createdAt: now,
          updatedAt: now,
          weights: { competencia:40, experiencia:30, formacao:15, localidade:15 },
          requisitos: []
        };
        state.vagas.unshift(v);
        state.selectedId = v.id;
        toast("Vaga criada.");
      }

      saveState();
      renderAreaFilter();
      updateKpis();
      renderList();
      renderDetail();

      bootstrap.Modal.getOrCreateInstance($("#modalVaga")).hide();
    }

    function deleteVaga(id){
      const v = findVaga(id);
      if(!v) return;

      const ok = confirm(`Excluir a vaga "${v.titulo}"?\n\nIsso remove tambÃ©m os requisitos.`);
      if(!ok) return;

      state.vagas = state.vagas.filter(x => x.id !== id);
      if(state.selectedId === id){
        state.selectedId = state.vagas[0]?.id || null;
      }
      saveState();
      renderAreaFilter();
      updateKpis();
      renderList();
      renderDetail();
      toast("Vaga excluÃ­da.");
    }

    function duplicateVaga(id){
      const v = findVaga(id);
      if(!v) return;

      const now = new Date().toISOString();
      const copy = JSON.parse(JSON.stringify(v));
      copy.id = uid();
      copy.codigo = (v.codigo ? v.codigo + "-COPY" : "");
      copy.titulo = (v.titulo ? v.titulo + " (CÃ³pia)" : "CÃ³pia");
      copy.createdAt = now;
      copy.updatedAt = now;
      // novos ids de requisitos
      (copy.requisitos || []).forEach(r => r.id = uid());

      state.vagas.unshift(copy);
      state.selectedId = copy.id;
      saveState();
      renderAreaFilter();
      updateKpis();
      renderList();
      renderDetail();
      toast("Vaga duplicada.");
    }

    // ========= CRUD: Requisitos
    function openReqModal(mode, vagaId, reqId){
      const v = findVaga(vagaId);
      if(!v) return;

      const modal = bootstrap.Modal.getOrCreateInstance($("#modalReq"));
      const isEdit = mode === "edit";

      $("#modalReqTitle").textContent = isEdit ? "Editar requisito" : "Novo requisito";
      $("#reqId").value = "";

      if(isEdit){
        const r = (v.requisitos || []).find(x => x.id === reqId);
        if(!r) return;

        $("#reqId").value = r.id;
        $("#reqCategoria").value = r.categoria || DEFAULT_CATEGORIA;
        $("#reqPeso").value = clamp(parseInt(r.peso ?? 0,10)||0, 0, 10);
        $("#reqObrigatorio").checked = !!r.obrigatorio;
        $("#reqTermo").value = r.termo || "";
        $("#reqSinonimos").value = (r.sinonimos || []).join(", ");
        $("#reqObs").value = r.obs || "";
      }else{
        $("#reqCategoria").value = DEFAULT_CATEGORIA;
        $("#reqPeso").value = 7;
        $("#reqObrigatorio").checked = false;
        $("#reqTermo").value = "";
        $("#reqSinonimos").value = "";
        $("#reqObs").value = "";
      }

      // guarda vaga atual no dataset do botÃ£o salvar
      $("#btnSaveReq").dataset.vagaId = vagaId;

      modal.show();
    }

    function saveReqFromModal(){
      const vagaId = $("#btnSaveReq").dataset.vagaId;
      const v = findVaga(vagaId);
      if(!v) return;

      const rid = $("#reqId").value || null;
      const categoria = ($("#reqCategoria").value || "").trim();
      const peso = clamp(parseInt($("#reqPeso").value,10)||0, 0, 10);
      const obrigatorio = !!$("#reqObrigatorio").checked;
      const termo = ($("#reqTermo").value || "").trim();
      const sinonimos = ($("#reqSinonimos").value || "")
        .split(",")
        .map(s => s.trim())
        .filter(Boolean);
      const obs = ($("#reqObs").value || "").trim();

      if(!termo){
        toast("Informe a palavra-chave/termo do requisito.");
        return;
      }

      if(rid){
        const r = (v.requisitos || []).find(x => x.id === rid);
        if(!r) return;

        r.categoria = categoria;
        r.peso = peso;
        r.obrigatorio = obrigatorio;
        r.termo = termo;
        r.sinonimos = sinonimos;
        r.obs = obs;
        toast("Requisito atualizado.");
      }else{
        v.requisitos = v.requisitos || [];
        v.requisitos.push({
          id: uid(),
          categoria,
          peso,
          obrigatorio,
          termo,
          sinonimos,
          obs
        });
        toast("Requisito adicionado.");
      }

      v.updatedAt = new Date().toISOString();
      saveState();
      renderList();
      renderDetail();
      bootstrap.Modal.getOrCreateInstance($("#modalReq")).hide();
    }

    function deleteReq(vagaId, reqId){
      const v = findVaga(vagaId);
      if(!v) return;

      const r = (v.requisitos || []).find(x => x.id === reqId);
      if(!r) return;

      const ok = confirm(`Excluir requisito "${r.termo}"?`);
      if(!ok) return;

      v.requisitos = (v.requisitos || []).filter(x => x.id !== reqId);
      v.updatedAt = new Date().toISOString();
      saveState();
      renderList();
      renderDetail();
      toast("Requisito removido.");
    }

    function toggleReqMandatory(vagaId, reqId){
      const v = findVaga(vagaId);
      if(!v) return;
      const r = (v.requisitos || []).find(x => x.id === reqId);
      if(!r) return;

      r.obrigatorio = !r.obrigatorio;
      v.updatedAt = new Date().toISOString();
      saveState();
      renderList();
      renderDetail();
      toast(r.obrigatorio ? "Requisito marcado como obrigatÃ³rio." : "Requisito marcado como nÃ£o obrigatÃ³rio.");
    }

    // ========= Pesos/Threshold
    function saveWeightsFromDetail(vagaId, fromMobile=false){
      const v = findVaga(vagaId);
      if(!v) return;

      const root = fromMobile ? $("#mobileDetailBody") : $("#detailHost");
      const threshold = clamp(parseInt(root.querySelector("#thresholdRange")?.value || "0",10) || 0, 0, 100);

      const w = {
        competencia: clamp(parseInt(root.querySelector("#w_competencia")?.value || "0",10) || 0, 0, 100),
        experiencia: clamp(parseInt(root.querySelector("#w_experiencia")?.value || "0",10) || 0, 0, 100),
        formacao: clamp(parseInt(root.querySelector("#w_formacao")?.value || "0",10) || 0, 0, 100),
        localidade: clamp(parseInt(root.querySelector("#w_localidade")?.value || "0",10) || 0, 0, 100)
      };

      v.threshold = threshold;
      v.weights = w;
      v.updatedAt = new Date().toISOString();

      saveState();
      renderList();
      renderDetail();
      toast("Pesos e match mÃ­nimo salvos.");
    }

    // ========= Simulador (keyword match)
function simulateMatch(vagaId, fromMobile=false){
      const v = findVaga(vagaId);
      if(!v) return;

      const root = fromMobile ? $("#mobileDetailBody") : $("#detailHost");
      const area = root.querySelector("#simResult");
      if(!area) return;
      area.replaceChildren();

      const text = normalizeText(root.querySelector("#simText")?.value || "");
      const reqs = v.requisitos || [];

      if(!text){
        const warn = cloneTemplate("tpl-vaga-sim-alert-warning");
        if(warn) area.appendChild(warn);
        return;
      }
      if(!reqs.length){
        const info = cloneTemplate("tpl-vaga-sim-alert-info");
        if(info) area.appendChild(info);
        return;
      }

      const totalPeso = reqs.reduce((acc, r)=> acc + clamp(parseInt(r.peso||0,10)||0,0,10), 0) || 1;
      let hitPeso = 0;

      const hits = [];
      const missMandatory = [];

      reqs.forEach(r => {
        const termo = normalizeText(r.termo || "");
        const syns = (r.sinonimos || []).map(normalizeText).filter(Boolean);
        const bag = [termo, ...syns].filter(Boolean);

        const found = bag.some(t => t && text.includes(t));
        const p = clamp(parseInt(r.peso||0,10)||0,0,10);

        if(found){
          hitPeso += p;
          hits.push(r);
        }else if(r.obrigatorio){
          missMandatory.push(r);
        }
      });

      let score = Math.round((hitPeso / totalPeso) * 100);
      if(missMandatory.length){
        score = Math.max(0, score - Math.min(40, missMandatory.length * 15));
      }

      const thr = clamp(parseInt(v.threshold||0,10)||0,0,100);
      const pass = score >= thr;

      const card = cloneTemplate("tpl-vaga-sim-result");
      if(!card) return;

      const badge = card.querySelector('[data-role="sim-badge"]');
      if(badge){
        badge.classList.add(pass ? "text-bg-success" : "text-bg-danger");
      }
      setText(card, "sim-badge-text", pass ? "Dentro" : "Fora");

      const progress = card.querySelector('[data-role="sim-progress"]');
      if(progress) progress.style.width = `${score}%`;
      setText(card, "sim-score", `${score}%`);
      setText(card, "sim-thr", `${thr}%`);
      setText(card, "sim-hits", hits.length);
      setText(card, "sim-miss", missMandatory.length);

      const missBlock = card.querySelector('[data-role="sim-miss-block"]');
      if(missBlock) missBlock.classList.toggle("d-none", !missMandatory.length);
      setText(card, "sim-miss-list", missMandatory.map(r => r.termo).join(", "));
      setText(card, "sim-hit-list", hits.length ? hits.map(r => r.termo).join(", ") : EMPTY_TEXT);

      area.appendChild(card);
    }

    function clearSimulation(fromMobile=false){
      const root = fromMobile ? $("#mobileDetailBody") : $("#detailHost");
      const ta = root.querySelector("#simText");
      const res = root.querySelector("#simResult");
      if(ta) ta.value = "";
      if(res) res.replaceChildren();
    }

    // ========= Import/Export
    function exportJson(){
      const payload = {
        version: 1,
        exportedAt: new Date().toISOString(),
        vagas: state.vagas
      };
      const json = JSON.stringify(payload, null, 2);
      // download client-side
      const blob = new Blob([json], { type: "application/json;charset=utf-8" });
      const url = URL.createObjectURL(blob);
      const a = document.createElement("a");
      a.href = url;
      a.download = "vagas_mvp_liotecnica.json";
      document.body.appendChild(a);
      a.click();
      a.remove();
      URL.revokeObjectURL(url);
      toast("ExportaÃ§Ã£o iniciada.");
    }

    function importJson(){
      const inp = document.createElement("input");
      inp.type = "file";
      inp.accept = "application/json";
      inp.onchange = () => {
        const file = inp.files && inp.files[0];
        if(!file) return;

        const reader = new FileReader();
        reader.onload = () => {
          try{
            const data = JSON.parse(reader.result);
            if(!data || !Array.isArray(data.vagas)) throw new Error("Formato invÃ¡lido.");
            // validaÃ§Ã£o simples
            state.vagas = data.vagas.map(v => ({
              id: v.id || uid(),
              codigo: v.codigo || "",
              titulo: v.titulo || "",
              area: v.area || "",
              modalidade: v.modalidade || DEFAULT_MODALIDADE,
              status: v.status || DEFAULT_STATUS,
              cidade: v.cidade || "",
              uf: v.uf || "",
              senioridade: v.senioridade || DEFAULT_SENIORIDADE,
              threshold: clamp(parseInt(v.threshold ?? 70,10)||70,0,100),
              descricao: v.descricao || "",
              createdAt: v.createdAt || new Date().toISOString(),
              updatedAt: v.updatedAt || new Date().toISOString(),
              weights: v.weights || { competencia:40, experiencia:30, formacao:15, localidade:15 },
              requisitos: Array.isArray(v.requisitos) ? v.requisitos.map(r => ({
                id: r.id || uid(),
                categoria: r.categoria || DEFAULT_CATEGORIA,
                termo: r.termo || "",
                peso: clamp(parseInt(r.peso ?? 0,10)||0,0,10),
                obrigatorio: !!r.obrigatorio,
                sinonimos: Array.isArray(r.sinonimos) ? r.sinonimos : [],
                obs: r.obs || ""
              })) : []
            }));

            state.selectedId = state.vagas[0]?.id || null;
            saveState();
            renderAreaFilter();
            updateKpis();
            renderList();
            renderDetail();
            toast("ImportaÃ§Ã£o concluÃ­da.");
          }catch(e){
            console.error(e);
            alert("Falha ao importar JSON. Verifique o arquivo.");
          }
        };
        reader.readAsText(file);
      };
      inp.click();
    }

    // ========= Init + bindings
    function wireClock(){
      const tick = () => {
        const d = new Date();
        $("#nowLabel").textContent = d.toLocaleString("pt-BR", {
          weekday:"short", day:"2-digit", month:"2-digit",
          hour:"2-digit", minute:"2-digit"
        });
      };
      tick();
      setInterval(tick, 1000 * 15);
    }

    function wireFilters(){
      const apply = () => {
        state.filters.q = ($("#fSearch").value || "").trim();
        state.filters.status = $("#fStatus").value || "all";
        state.filters.area = $("#fArea").value || "all";
        renderList();
      };

      $("#fSearch").addEventListener("input", apply);
      $("#fStatus").addEventListener("change", apply);
      $("#fArea").addEventListener("change", apply);

      $("#globalSearch").addEventListener("input", () => {
        $("#fSearch").value = $("#globalSearch").value;
        apply();
      });
    }

    function wireButtons(){
      $("#btnNewVaga").addEventListener("click", () => openVagaModal("new"));
      $("#btnSaveVaga").addEventListener("click", upsertVagaFromModal);
      $("#btnSaveReq").addEventListener("click", saveReqFromModal);

      $("#btnExportJson").addEventListener("click", exportJson);
      $("#btnImportJson").addEventListener("click", importJson);

      $("#btnSeedReset").addEventListener("click", () => {
        const ok = confirm("Restaurar dados de exemplo? Isso substitui suas vagas atuais no MVP.");
        if(!ok) return;
        state.vagas = [];
        state.selectedId = null;
        saveState();
        seedIfEmpty();
        renderAreaFilter();
        updateKpis();
        renderList();
        renderDetail();
        toast("Demo restaurada.");
      });
    }

    function initLogo(){
      $("#logoDesktop").src = LOGO_DATA_URI;
      $("#logoMobile").src = LOGO_DATA_URI;
    }

    (function init(){
      initLogo();
      wireClock();

      const has = loadState();
      if(!has) seedIfEmpty();
      else seedIfEmpty(); // caso tenha vindo vazio por algum motivo

      renderAreaFilter();
      updateKpis();
      renderList();
      renderDetail();

      wireFilters();
      wireButtons();

      // garantir que haja seleÃ§Ã£o
      if(!state.selectedId && state.vagas.length){
        state.selectedId = state.vagas[0].id;
        saveState();
        renderList();
        renderDetail();
      }
    })();




