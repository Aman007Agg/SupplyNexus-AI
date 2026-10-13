
# SUPPLYNEXUS AI
# PHASE 0 — PRODUCT DISCOVERY

*AI Intelligence for the Connected Supply Chain*

| Field | Value |
|---|---|
| Document | Phase 0 — Product Discovery |
| Repository path | `docs/product/phase-0-product-discovery.md` |
| Version | 0.1 (Discovery Draft) |
| Date | 28 September 2026 |
| Product owner | Aman Agrawal |
| SDLC stage | Stage 1 — Product |
| Phase status | **CONTINUE PRODUCT DISCOVERY** (see Section 31 and the Handoff) |
| Next phase | Phase 1 — Requirements & PRD (not started) |

### How to read this document — evidence labels

Every important statement carries a label so that hypotheses are never mistaken for research.

| Label | Meaning |
|---|---|
| **[SOURCE-DERIVED FACT]** | Stated directly in the SupplyNexus AI project material (project brief, Master SDLC file, Phase 0 prompts). These are *facts about the project's intent*, not facts about the market. |
| **[INFERENCE]** | A reasoned conclusion drawn from the source material. |
| **[ARCHITECTURAL RECOMMENDATION]** | A product or architecture recommendation made in this document. The product owner may accept or reject it. |
| **[GENERAL ENGINEERING KNOWLEDGE]** | Widely-held industry or engineering knowledge, not specific to SupplyNexus. |
| **[ASSUMPTION]** | Believed to be true but not evidenced. |
| **[TO VALIDATE]** | Must be confirmed with users, practitioners, experiments or data before it can be relied on. |
| **[UNKNOWN]** | Not established by any source. |

> **Important disclosure.** No customer interviews, user research, market statistics or competitive analysis have been performed or supplied. Nothing in this document should be read as customer evidence. All market, user, buyer and value statements are hypotheses.

---

## 1. Document Purpose

This document establishes **why** SupplyNexus AI should exist, **who** it is for, **what** problem it addresses, **what value** it could provide, **what** the initial product should and should not do, and **which assumptions** still require validation. [SOURCE-DERIVED FACT]

It is deliberately **not**:

- a PRD (Phase 1),
- a domain or SaaS business model (Phase 2),
- a system, data, AI, security or API architecture (Phases 3–7),
- an implementation, coding or deployment plan (Phase 8 onwards).

Technologies listed in the project brief (Angular, .NET, Python/FastAPI, PostgreSQL/pgvector, Amazon Bedrock, MCP, AgentCore, Kong, Redis, S3, Opik, OpenTelemetry, Docker, Terraform, GitHub Actions, AWS) are a **potential** direction and are **not** decided here. [SOURCE-DERIVED FACT] — the source explicitly states that "none of these technologies are automatically mandatory."

The document also serves a second, source-stated purpose: SupplyNexus AI is both a real SaaS product *and* the owner's AI-engineering learning platform. [SOURCE-DERIVED FACT] Where these two goals pull in different directions, this document says so explicitly rather than hiding the tension.

---

## 2. Product Identity

| Attribute | Value | Status |
|---|---|---|
| Product name | SupplyNexus AI | SOURCE-DERIVED FACT |
| Tagline | "AI Intelligence for the Connected Supply Chain" | SOURCE-DERIVED FACT |
| Category | Supply Chain Intelligence SaaS (multi-tenant) | SOURCE-DERIVED FACT |
| Delivery model | Multi-tenant SaaS | SOURCE-DERIVED FACT |
| Interaction model | Natural-language questions that are answered through the most appropriate governed means (knowledge, data, or investigation) | SOURCE-DERIVED FACT |
| Product nature | Decision-support / investigation tool — **not** a system of execution | INFERENCE |
| Dual purpose | Real SaaS product first, AI-engineering learning platform second | SOURCE-DERIVED FACT |
| Builder | Individual contributor | SOURCE-DERIVED FACT |

### Working product thesis (from source)

> *SupplyNexus AI is an enterprise supply-chain intelligence platform designed to help supply-chain teams understand, query and investigate fragmented operational data and knowledge through a governed AI interface that produces evidence-backed business insights.* [SOURCE-DERIVED FACT]

---

## 3. Product Vision

### 3.1 What SupplyNexus AI is

A governed, natural-language "intelligence layer" that sits **alongside** existing supply-chain systems (WMS, ERP, OMS, TMS, inventory, supplier and transportation systems, databases, APIs, spreadsheets, documents, object storage) and helps people **understand, query and investigate** what is happening in their operation. [SOURCE-DERIVED FACT]

### 3.2 The long-term problem it addresses

Supply-chain information is distributed across many systems and document stores. Answering an operational question — especially a *why* question — often requires a person to know which systems to look in, extract data, reconcile it, and apply domain knowledge. [INFERENCE from source's emphasis on "fragmented operational data and knowledge"]

### 3.3 What users should ultimately be able to accomplish

- Ask a factual question about their operation and receive a correct, traceable answer. [SOURCE-DERIVED FACT]
- Ask what a term, policy or procedure means *in their organization* and receive an answer grounded in their own documents. [INFERENCE]
- Ask *why* something happened (e.g. "Why was SKU-10023 oversold?") and receive an explanation supported by evidence the user can inspect. [SOURCE-DERIVED FACT]
- Eventually, receive recommendations — and, much later, controlled and approved actions. [SOURCE-DERIVED FACT for recommendations; controlled actions are future vision]

### 3.4 Why this is different from "building a chatbot"

| A generic chatbot | SupplyNexus AI (intended) |
|---|---|
| Answers from general model knowledge | Answers from the **tenant's** governed data and documents [SOURCE-DERIVED FACT] |
| Output is plausible text | Output is an answer **plus evidence** (records, documents, calculations) [SOURCE-DERIVED FACT] |
| One mechanism for every question | Distinguishes deterministic data retrieval, knowledge retrieval and investigation [SOURCE-DERIVED FACT] |
| Often unrestricted access to whatever it is connected to | Governed, read-only-first, tenant-scoped access; no arbitrary queries [SOURCE-DERIVED FACT] |
| Success = "sounds right" | Success = measurably correct, grounded, traceable [SOURCE-DERIVED FACT] |

### 3.5 Why evidence and governed access matter (product view)

In operations, a wrong number or wrong root cause leads to wrong actions (wrong reorder, wrong escalation, wrong supplier blamed). Therefore **trust is a product feature**, not an engineering nicety: users must be able to see *where* an answer came from, and organizations must be confident that the product cannot see or leak data it should not. [INFERENCE] [GENERAL ENGINEERING KNOWLEDGE]

### 3.6 Conceptual value flow (product view — not an architecture)

```
User question
   ↓
SupplyNexus AI  (understands what kind of question it is)
   ↓
AI reasoning    (only where reasoning is actually needed)
   ↓
Governed capabilities  (approved, permission-checked ways to get information)
   ↓
Supply-chain data / knowledge  (the customer's own systems and documents)
   ↓
Evidence        (the records, documents and calculations behind the answer)
   ↓
Business insight
   ↓
Recommended action  (a human decides)
```

### 3.7 How the product could evolve

**Understand** (knowledge) → **Query** (data) → **Investigate** (why) → **Recommend** → **Anticipate** (risk/anomaly) → **Act, with approval** (controlled actions). [INFERENCE from source future capabilities] Only the first three are candidates for MVP (Section 16); the rest are future vision (Section 27).

---

## 4. Problem Discovery

### 4.1 Problem, symptoms, root causes, consequences

| Layer | Description | Status |
|---|---|---|
| **Core problem** | Supply-chain teams struggle to get **timely, trustworthy answers** — especially explanations of *why* something happened — because the relevant data and knowledge are fragmented across systems, documents and people. | INFERENCE (supported by source thesis) |
| **Symptoms** | Repeated ad-hoc data requests; spreadsheet exports; "who knows how to check this?" questions; slow exception follow-up; different teams quoting different numbers for the same metric. | ASSUMPTION / TO VALIDATE |
| **Root causes** | (1) Data lives in multiple systems with different identifiers and timing; (2) domain knowledge lives in documents and in people's heads; (3) querying data requires SQL/BI skills or IT tickets; (4) cross-system investigation is manual; (5) fixed reports answer *known* questions, not ad-hoc ones. | INFERENCE + GENERAL ENGINEERING KNOWLEDGE |
| **Consequences** | Slower decisions; slower exception resolution (stockouts, overselling, backlog, delays); analyst time spent gathering rather than analysing; dependency on a few experts; inconsistent explanations to management and customers. | ASSUMPTION / TO VALIDATE |

### 4.2 Why these questions are hard to answer

1. **Systems are specialised.** WMS knows the warehouse, OMS knows orders, ERP knows purchasing/finance, TMS knows transport. A *why* question usually crosses at least two of them. [GENERAL ENGINEERING KNOWLEDGE]
2. **Identifiers and timing differ.** An order, shipment, SKU and warehouse may be keyed differently in each system, and each system updates on its own schedule. [GENERAL ENGINEERING KNOWLEDGE]
3. **Knowledge is unstructured.** Policies, SOPs, supplier agreements and definitions (e.g. how *this* company computes safety stock) live in documents. [INFERENCE]
4. **Skill barrier.** The person who has the question is often not the person who can write the query. [ASSUMPTION / TO VALIDATE]
5. **Investigation is a sequence, not a query.** Root-cause analysis requires forming hypotheses, pulling several pieces of evidence and correlating them. [INFERENCE]

### 4.3 What happens when information is distributed

- The *first* cost is **time to find** (which system? which report? who owns it?).
- The *second* is **time to reconcile** (numbers disagree; timestamps differ).
- The *third* is **risk of wrong conclusion** (the analysis is only as good as the data the analyst happened to find).
[INFERENCE] — magnitudes are **[UNKNOWN / TO VALIDATE]**.

### 4.4 Skeptical challenge — is the problem painful enough?

| | |
|---|---|
| **Problem** | The source asserts the problem (fragmentation) but provides no evidence of *pain magnitude* or *willingness to pay*. The discovery canvas itself marks "Actual customer pain magnitude" as **UNKNOWN**. |
| **Impact** | If pain is moderate, organizations will continue with BI dashboards, Excel and analysts; SupplyNexus becomes "nice to have." |
| **Example** | "How many orders were returned last month?" is already on most returns dashboards; automating it with AI adds little. |
| **Recommendation** | Anchor the product on **ad-hoc *why* questions and exceptions** (where fixed reports fail), not on questions that dashboards already answer. Validate pain with time-on-task evidence (Section 24). |
| **Alternative** | Position primarily as a self-service data-query tool for non-technical users. |
| **Trade-off** | Self-service querying is easier to build but more crowded and less differentiated; investigation is more valuable but harder, riskier and slower to build. |

---

## 5. Problem Statement

| Field | Statement |
|---|---|
| **Problem** | Supply-chain and operations teams cannot quickly obtain trustworthy answers and explanations about their operation because the needed data and knowledge are spread across multiple systems, reports, documents and people. Explaining *why* an exception occurred (oversell, stockout, backlog, delay, discrepancy) is especially slow and manual. |
| **Who experiences it** | Primarily supply-chain / operations analysts and the managers who depend on their answers. **[TO VALIDATE]** |
| **When it occurs** | When an ad-hoc question is not covered by an existing report; when an exception needs a root cause; when a definition or policy must be checked; when management needs an explanation quickly. **[INFERENCE / TO VALIDATE]** |
| **Current consequence** | Manual, multi-step investigation (find system → export → combine in Excel/SQL → ask other teams → reconcile → explain). **[CURRENT WORKFLOW HYPOTHESIS — Section 11]** |
| **Business impact** | Delayed decisions and exception resolution, analyst capacity consumed by data gathering, dependency on a few experts, inconsistent explanations. **[ASSUMPTION — magnitude UNKNOWN]** |
| **Evidence from source** | The product thesis ("understand, query and investigate fragmented operational data and knowledge"); example questions ("Why was SKU-10023 oversold?", "Which warehouse has the highest backlog?"); the discovery canvas ("Fragmented supply-chain information makes investigation difficult" — *hypothesis supported by source*). |
| **Confidence** | **Medium** that the problem exists in general (widely recognised in the industry — GENERAL ENGINEERING KNOWLEDGE); **Low** on who feels it most, how often, and how much they would pay. |
| **Validation required** | Who has the problem most acutely; frequency of ad-hoc/why questions; time per investigation; existing tools used; whether evidence-backed answers would change trust and adoption; who pays. |

---

## 6. Target Organizations

All entries below are a **TARGET MARKET HYPOTHESIS**. None is a confirmed market.

| Organization type | Why SupplyNexus might fit | Data / integration complexity | Status |
|---|---|---|---|
| **Mid-size to large distributors** | Many SKUs, multiple warehouses, suppliers and carriers; frequent inventory and fulfilment exceptions | Medium–High (ERP + WMS + TMS typical) | TARGET MARKET HYPOTHESIS |
| **Omnichannel retailers / e-commerce operators** | Overselling, returns, backlog and delivery delays are visible and customer-facing | Medium–High (OMS + WMS + marketplaces) | TARGET MARKET HYPOTHESIS |
| **3PLs (third-party logistics)** | Operate warehouses for many clients; must explain performance and exceptions to customers | High (multi-client WMS, SLA reporting) | TARGET MARKET HYPOTHESIS |
| **Manufacturers** | Supplier performance, procurement delays, component shortages | High (ERP/MRP heavy) | TARGET MARKET HYPOTHESIS |
| **Logistics / transportation organizations** | Shipment delays, carrier performance | Medium (TMS-centric) | TARGET MARKET HYPOTHESIS |
| **Warehouse operators** | Backlog, throughput, inventory accuracy | Medium (WMS-centric) | TARGET MARKET HYPOTHESIS |

### 6.1 Characteristics that likely make SupplyNexus valuable [INFERENCE / TO VALIDATE]

- **Three or more** operational systems that must be combined to answer common questions.
- **High exception volume** (oversells, stockouts, late shipments, discrepancies).
- A **dedicated analyst/ops team** fielding ad-hoc questions from managers.
- **Documented** policies/SOPs that people frequently need to consult.
- **Moderate data maturity**: data is accessible (database or API) but not fully unified.
- **Openness to governed AI** (security review is possible, not a blanket ban).

### 6.2 Characteristics that likely make SupplyNexus *less* valuable [INFERENCE]

- A single all-in-one system with strong built-in reporting.
- A mature, unified data warehouse with a strong BI/self-service culture.
- Very low data maturity (data not accessible; the problem is data quality, not access).
- Regulated environments that forbid external AI processing of operational data **[TO VALIDATE per organization]**.

### 6.3 Skeptical challenge — target organization is too broad

| | |
|---|---|
| **Problem** | Six organization types with different systems and problems cannot all be served by one MVP. |
| **Impact** | Unfocused use cases, generic data model, weak product-market fit signal. |
| **Example** | A 3PL's key question ("which client's SLA is at risk?") differs from a retailer's ("why was this SKU oversold?"). |
| **Recommendation** | Choose **one beachhead** for the MVP. The source examples (orders, returns, SKU overselling, warehouse backlog) most naturally fit an **omnichannel retailer / e-commerce operator or distributor running its own warehouses**. [ARCHITECTURAL RECOMMENDATION — TO VALIDATE] |
| **Alternative** | 3PL beachhead (multi-client explanation needs are strong). |
| **Trade-off** | The retail/distribution beachhead aligns with available public data (e.g. Olist is e-commerce order data — SOURCE-DERIVED FACT that Olist is a candidate dataset) but may be more crowded; the 3PL beachhead may have sharper pain but adds multi-client complexity on top of multi-tenancy. |

---

## 7. Target Users

| User | Classification | Rationale | Status |
|---|---|---|---|
| Supply Chain Analyst | **Primary User** | Fields ad-hoc questions, runs investigations, closest to the problem | TO VALIDATE (source canvas: "Initial user: supply-chain / operations analyst — TO VALIDATE") |
| Operations Analyst | **Primary User** | Same job-to-be-done in many organizations | TO VALIDATE |
| Inventory Manager | **Secondary User** | Owns inventory exceptions (oversell, stockouts, discrepancies) | TO VALIDATE |
| Warehouse Manager | **Secondary User** | Backlog, throughput, warehouse exceptions | TO VALIDATE |
| Supply Chain Manager / Operations Manager | **Secondary User** | Consumes answers; asks "why"; makes decisions | TO VALIDATE |
| Logistics Manager | **Occasional User** | Shipment delays; transport questions | TO VALIDATE |
| Procurement Manager | **Occasional User** | Supplier performance; PO delays | TO VALIDATE |
| Executive / Director | **Occasional User** (and potential buyer) | Needs summaries and explanations, not data work | TO VALIDATE |
| Data Analyst / Business Analyst | **Technical User** | May validate answers, curate definitions, compare with BI | TO VALIDATE |
| IT / Data Team | **Administrative User** | Connects sources, manages access, approves security | TO VALIDATE |
| Tenant Administrator | **Administrative User** | Manages users, roles, documents | INFERENCE (implied by multi-tenant SaaS) |
| Customer-service / account teams at 3PLs | **Potential Future User** | Explain exceptions to external clients | ASSUMPTION |

**Recommendation:** treat the **Supply Chain / Operations Analyst** as the single primary MVP persona, and the **Operations / Supply Chain Manager** as the primary *consumer* of the analyst's output. [ARCHITECTURAL RECOMMENDATION — TO VALIDATE]

---

## 8. Personas

> All personas are **hypothetical constructs** built from source examples and general industry knowledge. They are not derived from interviews. **[ASSUMPTION / TO VALIDATE]**

### 8.1 Persona A — "Priya", Supply Chain / Operations Analyst (Primary)

| Attribute | Description |
|---|---|
| Responsibilities | Monitor operational KPIs; answer ad-hoc questions from managers; investigate exceptions; prepare explanations and reports |
| Goals | Answer quickly and correctly; spend time on analysis instead of data gathering; be trusted |
| Pain points | Data spread across systems; repetitive extracts; reconciling conflicting numbers; slow access to other teams' data; urgent "why" requests |
| Current workflow | Dashboards → exports → Excel / SQL → emails/chats with other teams → explanation (Section 11) |
| Information needs | Orders, inventory positions and movements, warehouse status, shipments, returns, supplier performance, definitions/policies |
| Typical questions | "How many orders were returned last week?", "Which warehouse has the highest backlog?", "Why was SKU-10023 oversold?", "What is our safety-stock policy for fast movers?" |
| Decisions made / supported | Escalations, root-cause statements, recommendations to managers |
| Potential SupplyNexus value | Faster answers, fewer manual extracts, evidence packs she can forward, consistent definitions |

### 8.2 Persona B — "Daniel", Operations / Supply Chain Manager (Secondary; primary consumer)

| Attribute | Description |
|---|---|
| Responsibilities | Service levels, cost, backlog, escalations, team performance |
| Goals | Know what is wrong, why, and what to do — quickly |
| Pain points | Waiting for analysts; explanations that differ by source; unclear root causes |
| Current workflow | Reviews dashboards; asks analysts; joins exception calls |
| Information needs | Summaries, trends, root causes, risk of repeat |
| Typical questions | "Why are we late on shipments from WH-001?", "Is the backlog getting worse?" |
| Decisions | Resource allocation, escalations, supplier/carrier conversations, policy changes |
| Potential value | Direct self-service answers for simple questions; trustworthy explanations for complex ones |

### 8.3 Persona C — "Marcus", Inventory Manager (Secondary)

| Attribute | Description |
|---|---|
| Responsibilities | Stock accuracy, replenishment policy, overselling/stockout prevention |
| Pain points | Discrepancies between systems; timing issues between order capture and stock updates |
| Typical questions | "Why does WMS show 40 units but OMS allowed 55 orders?", "Which SKUs were oversold this week?" |
| Potential value | Faster discrepancy investigation with timeline evidence |

### 8.4 Persona D — "Elena", Warehouse Manager (Secondary)

| Attribute | Description |
|---|---|
| Responsibilities | Throughput, backlog, labour, dock scheduling |
| Typical questions | "Which warehouse has the highest backlog?", "What's driving today's backlog at WH-002?" |
| Potential value | Backlog drivers and comparisons without waiting for reports |

### 8.5 Persona E — "Sam", IT / Data Owner (Administrative / Technical)

| Attribute | Description |
|---|---|
| Responsibilities | Data access, integrations, security approval, cost |
| Concerns | Data leakage, uncontrolled queries against production systems, prompt injection, auditability, AI cost |
| Questions they ask of the product | "What exactly can it access? Is it read-only? Where does data go? Can I audit it? Can one tenant see another's data?" |
| Potential value | A governed alternative to users pasting data into unapproved public AI tools **[ASSUMPTION / TO VALIDATE]** |

### 8.6 Persona F — Executive / Director (Occasional; potential economic buyer)

| Attribute | Description |
|---|---|
| Interests | Service level, cost, working capital, customer satisfaction |
| Use | Summaries and explanations; rarely hands-on |
| Value | Faster, more consistent operational explanations; analyst productivity |

---

## 9. Buyer / Stakeholder Map

> No organizational structures are known. Every role below is **[TO VALIDATE]**; the economic buyer is **[UNKNOWN]**.

| Role | Likely holder (hypothesis) | Interest | Concern | Expected value | Potential objection |
|---|---|---|---|---|---|
| **Economic Buyer** | VP / Director of Supply Chain or Operations — **UNKNOWN** | Productivity, service levels, cost | ROI, adoption | Faster decisions; analyst capacity | "Our BI tool / ERP vendor already does this." |
| **Business Sponsor** | Head of Operations / Supply Chain Manager | Fewer escalations; faster root cause | Disruption to team | Time back; consistent answers | "My team won't trust AI answers." |
| **Primary User** | Supply-chain / operations analyst | Less manual work | Accuracy; being blamed for wrong AI answers | Evidence-backed answers | "I still have to verify everything." |
| **Technical Buyer** | Head of IT / Enterprise Architecture | Fit with landscape | Integration effort; vendor lock-in | Governed access layer | "Another integration to maintain." |
| **IT Owner** | Integration / platform team | Stable, supportable integrations | Load on production systems | Read-only, controlled access | "It must not query production directly." |
| **Data Owner** | Data / analytics team | Definition consistency | Conflicting metrics vs BI | Shared definitions | "Numbers must match our warehouse." |
| **Security Owner** | CISO / security team | Data protection | Leakage, prompt injection, tenant isolation, third-party AI processing | Auditability, least privilege | "Where is data processed? Is it used to train models?" |
| **AI Governance Owner** | Risk / compliance / AI governance (if exists) | Responsible AI | Hallucination, accountability | Evaluation evidence, human-in-the-loop | "How do you measure accuracy?" |
| **Procurement** | Procurement department | Terms, price, vendor risk | Vendor viability (a new product built by an individual) | Clear pricing | "Vendor is too early / too small." |
| **Executive Stakeholder** | COO / CSCO | Strategic visibility | Cost vs impact | Better operational decisions | "Show measurable outcomes." |

### 9.1 Skeptical challenge — buyer is unknown

| | |
|---|---|
| **Problem** | No buyer is identified; the source canvas marks Buyer and Willingness to pay as **UNKNOWN / TO VALIDATE**. |
| **Impact** | Without a buyer, value metrics and MVP priorities may optimise for the user's convenience rather than the outcome someone will pay for. |
| **Example** | Analysts may love natural-language querying; a VP may only pay for measurable reduction in exception-resolution time. |
| **Recommendation** | State an explicit **buyer hypothesis** for Phase 1: *Head of Supply Chain / Operations* at the beachhead organization, buying for **faster exception investigation and analyst productivity**. Mark it TO VALIDATE. [ARCHITECTURAL RECOMMENDATION] |
| **Alternative** | IT / data leader as buyer (positioning: "governed AI access layer"). |
| **Trade-off** | The business buyer values outcomes and domain depth; the IT buyer values governance and integration. Each implies different MVP emphasis. |


---

## 10. Supply Chain Pain Points

> Pain points are **hypotheses** derived from source examples and general industry knowledge. Severity and frequency are **[TO VALIDATE]**. "AI suitability" describes whether AI adds value *beyond* deterministic software.

| # | Pain point | Who experiences it | Current workaround (hypothesis) | Business consequence | Potential product opportunity | AI suitability | Validation requirement |
|---|---|---|---|---|---|---|---|
| P1 | **Overselling** (orders accepted beyond available stock) | Inventory Mgr, Analyst, Customer Service | Compare OMS orders with WMS/inventory snapshots manually | Cancellations, customer dissatisfaction, marketplace penalties | Evidence-backed oversell investigation (timeline of stock vs orders) | **High** for explanation; **none** for the stock arithmetic | How often; how long an investigation takes today |
| P2 | **Inventory discrepancies** between systems | Inventory Mgr, Analyst | Exports + Excel reconciliation | Wrong availability, wrong replenishment | Discrepancy investigation across sources | **Medium–High** (correlation/explanation) | Which systems disagree most; root-cause patterns |
| P3 | **Stockouts** | Inventory Mgr, Planner | Reports + supplier follow-up | Lost sales | Stockout explanation (demand spike? late PO? wrong policy?) | **Medium** | Is explanation or prediction the real need? |
| P4 | **Warehouse backlog** | Warehouse Mgr, Ops Mgr | WMS dashboards, shift reports | Late shipments, SLA breaches | Backlog comparison + driver explanation | **Low** for ranking (deterministic); **Medium** for *why* | Do dashboards already solve this? |
| P5 | **Shipment delays** | Logistics Mgr, Customer Service | TMS / carrier portals, emails | SLA breaches, customer complaints | Delay investigation across order, warehouse and carrier events | **Medium** | Carrier data availability |
| P6 | **Returns** volume and reasons | Analyst, Returns Mgr | Returns reports, Excel | Margin loss, quality issues | NL querying + reason analysis | **Low** for counts; **Medium** for reason text analysis | Are return reasons captured as text? |
| P7 | **Supplier performance / late POs** | Procurement Mgr | ERP reports, scorecards in Excel | Shortages, expediting cost | Supplier performance query + explanation | **Low–Medium** | Data availability |
| P8 | **SLA violations** | Ops Mgr, 3PL account teams | Manual SLA reports | Penalties, customer churn | SLA-breach explanation | **Medium** | Beachhead-dependent |
| P9 | **Demand spikes** | Planner, Inventory Mgr | Reports, intuition | Stockouts, overselling | Spike detection & explanation | **Medium** (future: anomaly detection) | Out of MVP (forecasting) |
| P10 | **Operational exceptions** in general | Analyst, Ops Mgr | Manual triage | Slow resolution | Exception investigation workflows | **High** (multi-step reasoning) | Which exception types matter most |
| P11 | **Ad-hoc reporting requests** | Analyst (receives), Managers (ask) | Analyst writes SQL / builds Excel | Analyst capacity consumed | Governed NL data querying | **Medium** (NL→query translation) | Volume of ad-hoc requests |
| P12 | **Data fragmentation** | All | Knowing "who to ask" | Slow, inconsistent answers | Unified governed access across sources | **Low** (integration problem, not an AI problem) | Number and type of systems |
| P13 | **Manual investigation** | Analyst | Multi-step manual process | Time, inconsistency, dependency on experts | Guided / agentic investigation with evidence | **High** | Time-on-task measurement |
| P14 | **Finding policy / SOP / definition knowledge** | Analyst, new staff, managers | Search shared drives; ask colleagues | Inconsistent application of policy | Grounded knowledge assistant over tenant documents | **High** (language, retrieval) | Do tenants have useful documents? |
| P15 | **Procurement / transportation investigation** | Procurement, Logistics | ERP/TMS reports | Cost, delays | Future domain investigations | **Medium** | Post-MVP |

**Observation.** The pain points with the **highest AI suitability** (P1, P10, P13, P14) are *explanation* and *knowledge* problems. Those with the **lowest** (P4 ranking, P6 counts, P12) are *deterministic* or *integration* problems where AI should not be the core. [INFERENCE]

---

## 11. Current Workflow Analysis

> **CURRENT WORKFLOW HYPOTHESIS** — constructed from source examples and general industry knowledge. Not observed. Must be validated through workflow observation (Section 24).

### 11.1 Hypothesised investigation workflow — "Why was SKU-10023 oversold?"

| Step | Activity | Characteristics |
|---|---|---|
| 1 | Receive the question (email/chat/meeting) from a manager or customer-service | Manual |
| 2 | Decide which systems hold relevant data (OMS, WMS, inventory, ERP) | Knowledge-heavy |
| 3 | Open reports/dashboards in each system | Manual, cross-system |
| 4 | Export order history and stock history for the SKU | Manual, repetitive, data-heavy |
| 5 | Combine exports in Excel or run SQL | Slow, error-prone, data-heavy |
| 6 | Notice inconsistencies (timing, identifiers, units) | Error-prone |
| 7 | Contact another team (warehouse, IT, e-commerce) for missing information | Slow, cross-system |
| 8 | Check policies (e.g. safety stock, allocation rules, channel buffers) | Knowledge-heavy |
| 9 | Form and test hypotheses (sync delay? receiving error? manual adjustment? demand spike?) | Reasoning-heavy |
| 10 | Prepare an explanation with supporting evidence | Manual, repetitive |
| 11 | Recommend or take action | Human decision |

### 11.2 Classification of workflow effort

| Characteristic | Steps | Product implication |
|---|---|---|
| Manual | 1, 3, 4, 7, 10 | Automation/assistance candidates |
| Repetitive | 4, 5, 10 | Deterministic automation first |
| Slow | 5, 7 | Governed access to data reduces waiting |
| Error-prone | 5, 6 | Deterministic calculations, consistent definitions |
| Cross-system | 2, 3, 7 | Integration value (not AI value) |
| Knowledge-heavy | 2, 8 | Knowledge retrieval (AI suitable) |
| Data-heavy | 4, 5 | Governed data access (AI only for intent translation) |
| Reasoning-heavy | 9 | Investigation (AI suitable, evidence required) |

### 11.3 Simple question workflow — "Which warehouse has the highest backlog?"

Question → open WMS dashboard → sort/filter → answer. **This may already be fast.** The product opportunity is only meaningful if the question is **not** on an existing dashboard, spans systems, or is asked by someone without dashboard access/skills. [INFERENCE — TO VALIDATE]

---

## 12. Existing Solutions

> **COMPETITIVE VALIDATION REQUIRED.** No competitive research was provided. The table describes solution *categories* using general industry knowledge. No claims are made about specific vendors or products.

| Existing approach | What it already solves well | Where friction may remain | What SupplyNexus could potentially improve |
|---|---|---|---|
| **ERP reports** | Standard transactional and financial reporting | Rigid; single-system; requires navigation expertise | Cross-system context; NL access |
| **WMS / OMS / TMS dashboards** | Operational monitoring within one domain | Siloed; answer "what", rarely "why" | Cross-system *why* explanations |
| **BI tools** (dashboards, self-service analytics) | Known KPIs, trends, governed metrics | Ad-hoc questions need skills or new dashboards; weak at multi-step investigation | Ad-hoc NL questions; investigation workflows; knowledge + data in one place |
| **SQL** | Precise, flexible | Requires skill and access; risky on production | Governed, validated query access for non-SQL users |
| **Excel** | Universal, flexible | Manual, error-prone, not auditable | Reproducible evidence |
| **Data warehouses / lakes** | Integrated, historical data | Still need a query/analysis layer; may lag operational reality | Could be a *source* for SupplyNexus rather than a competitor |
| **APIs** | System-to-system access | Developer-only | Governed capability layer for users |
| **Existing analytics / "control tower" style platforms** | End-to-end visibility, alerts | Cost, implementation effort; depth of explanation **[UNKNOWN]** | **COMPETITIVE VALIDATION REQUIRED** |
| **Search / internal knowledge bases** | Document search | Keyword search; no synthesis; no link to live data | Grounded answers with citations |
| **Traditional automation / rules / alerts** | Deterministic detection of known conditions | Detects *that*, not *why* | Explanation of detected exceptions |
| **Existing AI assistants** (general-purpose assistants; AI features embedded in BI, ERP and productivity tools) | NL interaction; general knowledge; some NL-to-query | Generic domain understanding; governance varies; cross-system investigation depth **[UNKNOWN]** | Supply-chain specificity, evidence, governance — **COMPETITIVE VALIDATION REQUIRED** |

### 12.1 Skeptical challenge — "embedded AI" threat

| | |
|---|---|
| **Problem** | Vendors of ERP, WMS and BI platforms are adding natural-language and AI assistant features to their own products [GENERAL ENGINEERING KNOWLEDGE]. Simple NL querying may become a commodity feature. |
| **Impact** | A Data Assistant alone may not justify a separate product. |
| **Example** | A customer whose BI tool offers NL querying asks, "why do I need SupplyNexus to count returns?" |
| **Recommendation** | Differentiate on **cross-system investigation with evidence** and **supply-chain-specific knowledge**, not on NL querying alone. Validate against real alternatives before Phase 1 is locked. [ARCHITECTURAL RECOMMENDATION] |
| **Alternative** | Accept commodity NL querying as table-stakes and compete on domain depth and governance. |
| **Trade-off** | Investigation is harder and slower to build, but is where the defensible value likely lies. |

---

## 13. AI Opportunities

| Capability | 1. Problem solved | 2. Why AI is useful | 3. Could deterministic software solve it? | 4. Risk of using AI | 5. Evidence required | 6. MVP? |
|---|---|---|---|---|---|---|
| **Knowledge retrieval** (tenant SOPs, policies, definitions) | Finding and understanding organization-specific knowledge | Language understanding; synthesis across documents | Partially (keyword search finds documents; does not answer) | Hallucination; outdated documents; indirect prompt injection from documents | Cited passages / document references | **Yes — narrow corpus** |
| **Generic supply-chain knowledge** ("What is safety stock?") | Education / definitions | Fluent explanations | N/A — but a general-purpose assistant already does this | Low differentiation; answer may contradict tenant's own definition | Source documents if tenant-specific | **Only as part of tenant knowledge** (see 13.1) |
| **Natural-language querying** | Non-SQL users need ad-hoc answers | Translates intent into a governed query | Partly: parameterised reports and filters cover many questions | Wrong filter/time range → confidently wrong number | The executed query/parameters and result rows | **Yes — for bounded question families** |
| **Data analysis** (ranking, trends, comparisons) | Summarising data | Explaining results in language | **Yes** — calculations must be deterministic | AI doing arithmetic incorrectly | Deterministic computation + data | AI **only** for intent and narration |
| **Root-cause investigation** | "Why did X happen?" | Hypothesis generation; multi-step evidence gathering; correlation | Partly: fixed diagnostic playbooks for known exception types | Plausible but wrong root cause; runaway cost; over-reach | Timeline, records and checks supporting each claim | **Yes — one bounded exception type** |
| **Reasoning / correlation** | Connecting evidence across systems | Flexible reasoning over heterogeneous evidence | Partially, for known patterns | Spurious correlation | Explicit evidence links | Within investigation only |
| **Recommendations** | "What should I do?" | Suggests next steps | Rules for known cases | Accountability; wrong advice | Evidence + policy references | **No (future)** — MVP may suggest *next investigation steps* only |
| **Summarization** | Condensing evidence and findings | Language generation | No | Omission of important facts | Link back to full evidence | Yes, as part of answers |
| **Exception investigation** | Triage of exceptions | Reasoning over varied cases | Rules detect exceptions | Same as investigation | Same | One type in MVP |
| **Cross-system investigation** | Joining evidence across systems | Reasoning over heterogeneous sources | Integration is deterministic; reasoning is not | Identifier mismatch → wrong conclusions | Source attribution per evidence item | Limited in MVP (single synthetic dataset) |
| **Natural-language interface** | Lower skill barrier | Language | Forms/filters can serve many needs | Ambiguity; misinterpretation | Show interpretation back to user | Yes |

### 13.1 Skeptical challenge — "What is safety stock?"

| | |
|---|---|
| **Problem** | The source's canonical knowledge example is a *generic* definition that any general-purpose AI assistant or textbook answers. |
| **Impact** | A Knowledge Assistant built around generic definitions has little product value. |
| **Example** | A user already gets a good definition of safety stock from a public assistant for free. |
| **Recommendation** | Re-frame the Knowledge capability as **"What is *our* safety-stock policy for category X, and where is it written?"** — tenant-specific, cited knowledge. [ARCHITECTURAL RECOMMENDATION] |
| **Alternative** | Keep generic knowledge as an onboarding / education feature. |
| **Trade-off** | Tenant-specific knowledge requires tenants to have useful documents (TO VALIDATE) and raises document-security concerns; generic knowledge is easy but weakly differentiated. |

---

## 14. Non-AI Opportunities

**Principle:** Use AI where reasoning, language or ambiguity creates value. Use deterministic software where deterministic software is more reliable. [SOURCE-DERIVED FACT — "AI must not replace deterministic software where deterministic logic is sufficient."]

| Area | Why deterministic | Status |
|---|---|---|
| **KPI and metric calculations** (return counts, backlog, fill rate, on-time %) | Must be exact, reproducible and consistent with BI | SOURCE-DERIVED FACT (principle) / INFERENCE (application) |
| **Filtering, sorting, ranking** ("highest backlog") | Pure data operations | INFERENCE |
| **Authentication** | Security-critical | SOURCE-DERIVED FACT |
| **Authorization / permission checks / tenant isolation** | Must never depend on model behaviour | SOURCE-DERIVED FACT |
| **Data validation** (inputs, identifiers like SKU-10023, ORD-458921) | Exact-match rules | SOURCE-DERIVED FACT (exact identifiers noted in source) |
| **Business-rule enforcement** (allocation rules, safety-stock formulas) | Deterministic business logic | INFERENCE |
| **Transaction processing / write operations** | Out of scope for MVP; must be explicit and approved | SOURCE-DERIVED FACT ("start with read-only") |
| **Query safety** (allowlists, read-only, no arbitrary queries) | Deterministic guardrails | SOURCE-DERIVED FACT |
| **Routing of high-confidence requests** | Rules first, model only when uncertain | SOURCE-DERIVED FACT |
| **Exception detection for known conditions** (e.g. orders > available stock) | Rule-based detection is exact | INFERENCE |
| **Audit logging, rate limiting** | Governance controls | SOURCE-DERIVED FACT |

**Implication for the product:** In SupplyNexus, **AI interprets and explains; deterministic software computes, validates, authorizes and enforces.** [ARCHITECTURAL RECOMMENDATION]

---

## 15. Core Use Cases

### 15.1 Minimum use cases

| Field | UC-1 Supply Chain Knowledge | UC-2 NL Data Query | UC-3 Inventory Investigation (Oversell) | UC-4 Order Investigation | UC-5 Warehouse Investigation |
|---|---|---|---|---|---|
| **Problem** | Policies/SOPs/definitions hard to find and interpret | Ad-hoc data questions need SQL/BI skills | Explaining oversells/discrepancies is slow and manual | Explaining why a specific order was late/cancelled/split | Explaining backlog and throughput issues |
| **User** | Analyst, managers, new staff | Analyst, managers | Analyst, Inventory Mgr | Analyst, Customer Service | Warehouse Mgr, Ops Mgr |
| **Trigger** | "What is our policy for…?" | "How many orders were returned last month?" | "Why was SKU-10023 oversold?" | "Why is ORD-458921 late?" | "Which warehouse has the highest backlog and why?" |
| **Current workflow** | Search drives / ask colleagues | Dashboards / request to analyst / SQL | Section 11.1 | Look up order in OMS, WMS, carrier | WMS dashboards + shift reports |
| **Desired outcome** | Cited, tenant-specific answer | Correct number with visible query interpretation | Explanation + evidence timeline + confidence | Order timeline + cause | Ranking (deterministic) + driver explanation |
| **Potential value** | Time saved; consistent policy application | Self-service; analyst time saved | Faster root cause; fewer repeat incidents | Faster customer responses | Faster operational response |
| **AI suitability** | High | Medium (translation only) | High | Medium–High | Low (ranking) / Medium (why) |
| **Data requirements** | Tenant documents | Orders, returns, inventory, warehouse data | Order events, stock levels/movements over time, channel allocations, policies | Order, fulfilment, shipment events | Warehouse tasks, order backlog, capacity |
| **Evidence requirements** | Document citations | Query interpretation + result rows | Time-ordered stock vs order evidence; rule/policy references | Event timeline | Metrics + contributing factors |
| **Complexity** | Medium | Medium | High | Medium–High | Medium |
| **MVP suitability** | **Yes (narrow)** | **Yes (bounded families)** | **Yes — flagship investigation** | Candidate (second investigation) | Ranking yes (via UC-2); *why* later |
| **Validation status** | TO VALIDATE | TO VALIDATE | TO VALIDATE | TO VALIDATE | TO VALIDATE |

### 15.2 Additional use cases evaluated

| Use case | Summary | MVP suitability | Reason |
|---|---|---|---|
| Shipment Investigation | Why shipment SHP-98231 is delayed | Later | Requires carrier/TMS data not in initial datasets **[INFERENCE]** |
| Supplier Investigation | Why supplier performance dropped | Later | Requires procurement/ERP data |
| Returns Investigation | Why returns spiked for a category | Candidate (post-MVP) | Return-reason text analysis is AI-suitable; data availability TO VALIDATE |
| Procurement Investigation | Why PO-100234 is late; ASN mismatch | Later | ERP/EDI (e.g. EDI 856 / ASN) complexity |
| Transportation Investigation | Carrier performance, lane delays | Later | TMS data |
| Risk Investigation | Which SKUs/suppliers are at risk | Future | Borders on prediction/anomaly detection |

---

## 16. MVP Discovery

### 16.1 Evaluation of the starting hypothesis

> **STARTING HYPOTHESIS** [SOURCE-DERIVED FACT]: (A) Knowledge Assistant → RAG; (B) Data Assistant → governed data/SQL tools; (C) Investigation Assistant → governed tools / MCP / agentic investigation. The source also states: "Do not implement all three immediately."

Note: the technology arrows (RAG, SQL, MCP, agents) are **candidate implementations**; product capabilities are evaluated here independently of them.

#### A. Knowledge Assistant

| Dimension | Assessment |
|---|---|
| User value | Moderate if tenant-specific; low if generic |
| Business value | Moderate (consistency, onboarding, time to find policy) |
| Complexity | Medium |
| AI requirement | Genuine (language + retrieval) |
| Data requirement | Tenant documents (availability TO VALIDATE) |
| Risk | Hallucination, stale documents, document-borne prompt injection |
| Demonstrability | High |
| Learning value | High (retrieval, grounding, evaluation) |
| **Strong justification** | Real AI need; needed anyway to give investigations policy context |
| **Weak justification** | Generic definitions are commodity; alone it is "chat with documents" |
| **Unknowns** | Do target organizations have usable, current SOP/policy documents? |
| **Dependencies** | Document corpus (synthetic/public for MVP) |
| **Validation** | Ask practitioners how often they look up policies and how |

#### B. Data Assistant

| Dimension | Assessment |
|---|---|
| User value | High for non-SQL users; low for users with good dashboards |
| Business value | Moderate–High (analyst capacity) |
| Complexity | Medium–High (correctness, governance) |
| AI requirement | Partial — AI translates intent; computation is deterministic |
| Data requirement | Structured operational data |
| Risk | Confidently wrong numbers; commoditisation by embedded BI AI |
| Demonstrability | High |
| Learning value | High (governed data access, validation) |
| **Strong justification** | Foundation for investigation (investigations need governed data access); frequent need |
| **Weak justification** | Overlaps heavily with BI; alone is weakly differentiated |
| **Unknowns** | Volume of ad-hoc questions; tolerance for errors |
| **Dependencies** | Data model and dataset |
| **Validation** | Measure time-to-answer for ad-hoc questions today |

#### C. Investigation Assistant

| Dimension | Assessment |
|---|---|
| User value | Potentially highest (addresses the slowest workflow) |
| Business value | Potentially highest (faster exception resolution) |
| Complexity | High |
| AI requirement | Strongest genuine need (multi-step reasoning) |
| Data requirement | Time-series / event data across domains (not fully present in Olist — INFERENCE) |
| Risk | Wrong root cause, cost, latency, unbounded scope, trust |
| Demonstrability | Very high |
| Learning value | Very high |
| **Strong justification** | Core differentiation hypothesis; where fixed reports fail |
| **Weak justification** | "Investigate anything" is unbounded and unevaluable |
| **Unknowns** | Which exception type is most painful; what evidence users need to trust a root cause |
| **Dependencies** | **Depends on B** (governed data access) and benefits from **A** (policy context) |
| **Validation** | Walk through 3–5 real or realistic oversell cases with practitioners |

### 16.2 Answers to the required discovery questions

| Question | Answer | Status |
|---|---|---|
| Should all three capabilities exist? | Yes, in the **long-term product**. | INFERENCE |
| Should one be removed? | No — but **generic** knowledge should be removed from the MVP value proposition (keep tenant-specific knowledge). | ARCHITECTURAL RECOMMENDATION |
| Should one be delayed? | **Open-ended** investigation should be delayed. A **single bounded** investigation type should stay in MVP, otherwise the MVP collapses into "chat with BI + chat with docs," which is weakly differentiated. | ARCHITECTURAL RECOMMENDATION |
| Should the order change? | **Product-value order**: Data → Investigation → Knowledge. **Dependency/learning build order** (current SDLC: RAG → Data/SQL → MCP → Agents) is acceptable because Investigation depends on the other two. These are different orders and both are valid. | ARCHITECTURAL RECOMMENDATION |
| Should the MVP be smaller? | **Yes.** One beachhead, one primary persona, bounded question families, one investigation type, one dataset. | ARCHITECTURAL RECOMMENDATION |
| Is another capability more valuable? | Possibly **deterministic exception detection** ("show me today's oversells") as the entry point into investigation — it tells users *what* to investigate. It is non-AI and cheap. | ARCHITECTURAL RECOMMENDATION — TO VALIDATE |
| Is the problem sufficiently painful? | **Unknown.** | TO VALIDATE |
| Is AI actually required? | For knowledge synthesis and investigation reasoning — yes. For counts, rankings, KPIs, security — **no**. | INFERENCE |
| Where should deterministic software be preferred? | See Section 14. | SOURCE-DERIVED FACT + INFERENCE |
| What must be validated with real users? | Primary user, beachhead, most painful exception type, evidence needs, trust threshold, buyer. | TO VALIDATE |

---

## 17. Initial MVP Boundary

**Proposed boundary** [ARCHITECTURAL RECOMMENDATION — pending owner decision and validation]:

> **The SupplyNexus AI MVP helps a supply-chain/operations analyst at a single beachhead organization type (hypothesis: omnichannel retailer or distributor operating its own warehouses) to (1) get correct, traceable answers to a bounded set of operational data questions, (2) get cited answers from their organization's own supply-chain policies and procedures, and (3) investigate one exception type — inventory overselling — with an evidence-backed explanation. All access is read-only, tenant-scoped and governed. A human always decides what to do.**

| MVP element | Included | Explicit limit |
|---|---|---|
| **Governed Data Q&A** | Orders, returns, inventory positions, warehouse backlog | Bounded question families; deterministic computation; interpretation shown to user |
| **Tenant Knowledge Q&A** | Curated corpus of policies/SOPs/glossary (synthetic/public for MVP) | Cited answers only; "I don't know" when not grounded |
| **Investigation** | **One** type: inventory overselling (e.g. "Why was SKU-10023 oversold?") | Read-only; evidence-backed; no recommendations beyond suggested next checks |
| **Exception list (deterministic)** | Optional candidate: list of detected oversells to investigate | TO VALIDATE whether needed in MVP |
| **Trust features** | Evidence and source shown with every answer | — |
| **Tenancy & security** | Multi-tenant from the start; tenant-scoped everything | SOURCE-DERIVED requirement |
| **Data** | Public / synthetic / safe datasets only | No confidential enterprise data [SOURCE-DERIVED FACT] |

**Why this boundary:**

1. It preserves the **differentiation hypothesis** (evidence-backed investigation) while keeping scope evaluable.
2. It respects the **dependency chain** (investigation needs governed data + policy context).
3. It is **buildable by an individual contributor** [SOURCE-DERIVED constraint].
4. Every element is **measurable** (correctness, groundedness, investigation success).
5. Oversell is the source's own flagship example and is well-suited to deterministic, seeded synthetic data. [INFERENCE]

**Data caveat.** Olist (a candidate dataset in the source) is e-commerce order data; it does not, to general knowledge, include warehouse stock-movement history needed to explain overselling. The MVP will likely require **deterministic synthetic inventory and warehouse data with seeded, known root causes** — which also makes investigations *evaluable* against known answers. [INFERENCE — TO VALIDATE in Phase 4]


---

## 18. Product Scope

> This is **product** scope (what the product does for users), not implementation scope (what gets built in which phase).

### 18.1 In scope — current product hypothesis

| Item | Status |
|---|---|
| Natural-language questions about supply-chain operations | SOURCE-DERIVED FACT |
| Distinguishing knowledge questions, data questions and investigations | SOURCE-DERIVED FACT |
| Governed, read-only access to supply-chain data | SOURCE-DERIVED FACT |
| Tenant-specific knowledge retrieval with citations | ARCHITECTURAL RECOMMENDATION (re-framing of source's Knowledge Assistant) |
| Evidence-backed investigation of a bounded exception type | SOURCE-DERIVED FACT (investigation) + ARCHITECTURAL RECOMMENDATION (bounded) |
| Evidence shown with answers | SOURCE-DERIVED FACT |
| Multi-tenant SaaS with tenant isolation | SOURCE-DERIVED FACT |
| Measurable AI quality (accuracy, groundedness, investigation success) | SOURCE-DERIVED FACT |
| Public / synthetic / safe data for initial product | SOURCE-DERIVED FACT |
| Domains: orders, returns, inventory, warehouse | INFERENCE (from source examples) |

### 18.2 Out of scope — current product hypothesis

| Item | Reason | Status |
|---|---|---|
| Write operations / actions in customer systems | Read-only first; writes need explicit approval & audit | SOURCE-DERIVED FACT |
| Shipment, supplier, procurement, transportation domains | Data availability; focus | ARCHITECTURAL RECOMMENDATION |
| Forecasting, predictive insights, what-if analysis | Future vision; requires foundational intelligence first | ARCHITECTURAL RECOMMENDATION |
| Open-ended "investigate anything" | Unbounded, unevaluable | ARCHITECTURAL RECOMMENDATION |
| Connectors to real enterprise systems (WMS/ERP/OMS/TMS) | Long-term vision; MVP uses safe datasets | SOURCE-DERIVED FACT (vision) + INFERENCE (MVP) |
| Billing, quotas, self-service onboarding | Phase 23 (SaaS Readiness) | SOURCE-DERIVED FACT (SDLC) |
| Confidential enterprise data | Explicitly prohibited | SOURCE-DERIVED FACT |

---

## 19. Non-Goals

| # | Non-goal | Basis |
|---|---|---|
| NG-1 | **Not** an ERP, WMS, OMS or TMS replacement; SupplyNexus sits alongside systems of record | INFERENCE from "connect to" systems (source) |
| NG-2 | **No** autonomous supply-chain execution | SOURCE-DERIVED (read-only first; writes require approval) |
| NG-3 | **No** unrestricted database access or arbitrary queries by AI | SOURCE-DERIVED FACT |
| NG-4 | **No** fully autonomous business decisions — humans remain accountable | ARCHITECTURAL / PRODUCT RECOMMENDATION (consistent with source human-in-the-loop direction) |
| NG-5 | **Not** a generic consumer chatbot or general-purpose assistant | SOURCE-DERIVED ("not simply an AI demo"; "not a chatbot" — INFERENCE) |
| NG-6 | **Not** every supply-chain domain at once | SOURCE-DERIVED ("do not build every possible supply-chain domain") |
| NG-7 | **No** predictive forecasting before foundational intelligence exists | ARCHITECTURAL / PRODUCT RECOMMENDATION |
| NG-8 | **Not** a showcase of every AI technique; no technology added only because it is popular | SOURCE-DERIVED FACT |
| NG-9 | **Not** a replacement for BI dashboards for known, recurring KPIs | ARCHITECTURAL / PRODUCT RECOMMENDATION |
| NG-10 | **No** AI-performed arithmetic for business metrics | ARCHITECTURAL / PRODUCT RECOMMENDATION (consistent with source deterministic principle) |
| NG-11 | **No** cross-tenant data sharing or cross-tenant answers (including cached answers) | SOURCE-DERIVED FACT |

---

## 20. Product Differentiation Hypotheses

> None of these is claimed as unique. All require **COMPETITIVE VALIDATION**.

| # | Differentiation hypothesis | Why it may matter | Potential competitor overlap | What must be validated |
|---|---|---|---|---|
| D1 | **Investigation, not just chat** — answers *why*, not only *what* | Fixed reports and dashboards rarely explain root causes | Control-tower / analytics platforms; AI features in BI | Do users value explanations enough to adopt a new tool? |
| D2 | **Evidence-backed answers** | Operational decisions need trust; evidence enables verification | Many AI tools show citations for documents; fewer for data investigations **[UNKNOWN]** | What evidence format makes users trust an answer? |
| D3 | **Governed access** (read-only, allowlisted, tenant-scoped, audited) | Security owners must approve AI access to operational data | Enterprise AI platforms emphasise governance | Is governance a buying criterion or a minimum requirement? |
| D4 | **Supply-chain-specific intelligence** (domain terms, identifiers such as SKU/ORD/SHP/WH/PO/ASN/EDI 856) | Generic tools misunderstand domain language and identifiers | Vertical supply-chain platforms | Does domain specificity measurably improve answers? |
| D5 | **Knowledge + data + investigation in one experience** | Real questions mix policy and data | BI + separate document search | Do users actually ask mixed questions? |
| D6 | **Right tool for the question** (deterministic where possible, AI where needed) | Correctness and cost | Emerging in advanced AI products | Is this visible/valuable to users, or only an internal quality property? |
| D7 | **Cross-system investigation** | The hardest part of today's workflow is crossing systems | Data platforms, integration vendors | Integration effort in real customers |
| D8 | **Explainable findings with confidence** | Accountability and auditability | — **[UNKNOWN]** | Would users act on a "medium-confidence" finding? |
| D9 | **Tenant-aware enterprise AI** | SaaS trust | Most enterprise SaaS | Likely table-stakes rather than a differentiator (INFERENCE) |

**Skeptical note.** D3 and D9 are more likely **requirements to enter** the market than reasons to buy. The strongest differentiation candidates are **D1 + D2 + D4** combined. [INFERENCE]

---

## 21. Business Value Hypotheses

> **No numerical ROI is claimed.** All magnitudes are **UNKNOWN / TO VALIDATE**.

| # | Value hypothesis | Who benefits | How it could be measured | Status |
|---|---|---|---|---|
| V1 | Reduced time to investigate an exception (e.g. an oversell) | Analyst, Ops Mgr | Time-on-task, before vs after | UNKNOWN / TO VALIDATE |
| V2 | Increased analyst productivity (fewer manual extracts) | Analyst, Buyer | Ad-hoc requests handled per week | UNKNOWN / TO VALIDATE |
| V3 | Faster operational decisions | Managers | Time from question to decision | UNKNOWN / TO VALIDATE |
| V4 | Reduced manual reporting | Analyst | Hours spent on ad-hoc reports | UNKNOWN / TO VALIDATE |
| V5 | Faster root-cause analysis → fewer repeat incidents | Ops, Inventory | Repeat-incident rate | UNKNOWN / TO VALIDATE |
| V6 | Reduced information-search effort for policies | All users | Time to find a policy answer | UNKNOWN / TO VALIDATE |
| V7 | Better visibility across systems | Managers | User-reported confidence; survey | UNKNOWN / TO VALIDATE |
| V8 | Improved exception handling consistency | Ops | Variance in explanations; audit | UNKNOWN / TO VALIDATE |
| V9 | Reduced shadow-AI risk (users pasting data into unapproved tools) | Security owner | Policy compliance | ASSUMPTION / TO VALIDATE |

---

## 22. Success Metrics

> Metrics are defined here; **no targets** are set. Any number below is labelled **PROPOSED TARGET** and is for discussion only.

| Category | Metric | Definition (discovery-level) | Target |
|---|---|---|---|
| **Business** | Investigation time | Time from question to evidence-backed explanation | UNKNOWN — baseline first |
| Business | Decision time | Time from exception to decision | UNKNOWN |
| Business | Manual effort | Manual extracts/reconciliations per investigation | UNKNOWN |
| **User** | Adoption | Active users / invited users | UNKNOWN |
| User | Task completion | % of sessions where the user got a usable answer | UNKNOWN |
| User | Repeat usage | Users returning weekly | UNKNOWN |
| User | Trust | % of answers users mark as useful/correct; evidence opened | UNKNOWN |
| **Product** | Questions answered | By type (knowledge / data / investigation) | — |
| Product | Investigations completed | Investigations reaching an evidence-backed conclusion | — |
| Product | Evidence usage | % of answers where the user inspected evidence | — |
| Product | Correct refusal | % of out-of-scope/unanswerable questions correctly declined | — |
| **AI** | Groundedness / faithfulness | Knowledge answers supported by cited sources | PROPOSED TARGET: set after baseline on golden dataset |
| AI | Retrieval accuracy | Relevant document retrieved for knowledge questions | PROPOSED TARGET: set after baseline |
| AI | Data answer correctness | Answer matches known correct result on golden questions | PROPOSED TARGET: very high bar — wrong numbers are unacceptable |
| AI | Question-type routing correctness | Right capability chosen | PROPOSED TARGET: set after baseline |
| AI | Tool correctness | Right governed capability with right parameters | PROPOSED TARGET: set after baseline |
| AI | Investigation success | Correct root cause on seeded synthetic cases | PROPOSED TARGET: set after baseline |
| AI | Safety | Prompt-injection resistance; zero cross-tenant leakage | **Zero** cross-tenant leakage is a requirement, not a target (SOURCE-DERIVED FACT) |
| **Operational** | Latency | Time to answer by question type | UNKNOWN — set in Phase 1/5 |
| Operational | Cost per request | Model + infrastructure cost per answer | UNKNOWN — set in Phase 1/5 |
| Operational | Failure rate | Errors / requests | UNKNOWN |

---

## 23. Assumptions Register

| ID | Assumption | Why we believe it | Impact if false | Confidence | Validation method | Status |
|---|---|---|---|---|---|---|
| A-01 | Supply-chain information is fragmented across multiple systems in target organizations | Source thesis; general industry knowledge | Core problem weakens | Medium | Practitioner interviews | ASSUMPTION |
| A-02 | Ad-hoc and *why* questions are frequent and time-consuming | Source examples | Low usage; weak value | Low | Interviews; time-on-task | TO VALIDATE |
| A-03 | The supply-chain/operations analyst is the primary user | Source canvas | Wrong UX and priorities | Low–Medium | Interviews | TO VALIDATE |
| A-04 | Head of Supply Chain/Operations is the economic buyer | Inference | Wrong value messaging | Low | Buyer conversations | UNKNOWN |
| A-05 | Users will trust AI answers if evidence is shown | Inference | Adoption fails | Low | Prototype testing | TO VALIDATE |
| A-06 | Existing BI/dashboards do not adequately answer *why* questions | General knowledge | Differentiation weakens | Medium | Tool walkthroughs | TO VALIDATE |
| A-07 | Overselling is a sufficiently painful and frequent exception | Source example | Wrong flagship investigation | Low | Interviews | TO VALIDATE |
| A-08 | Target organizations have useful policy/SOP documents | Inference | Knowledge capability low value | Low | Interviews; document samples | TO VALIDATE |
| A-09 | Security teams will approve governed AI access to operational data | Inference | Sales blocked | Low | Security-owner conversations | TO VALIDATE |
| A-10 | Synthetic/public data can realistically demonstrate value | Source (public/synthetic data) | Demo not convincing | Medium | Prototype review with practitioners | TO VALIDATE |
| A-11 | AI cost per request can be kept acceptable | Inference | Unviable economics | Unknown | Phase 5 cost modelling | UNKNOWN |
| A-12 | A single individual can build a credible MVP within the bounded scope | Source constraint | MVP never ships | Medium (if bounded) | Phase 1 scoping | ASSUMPTION |
| A-13 | Multi-tenancy is required from MVP | Source principle | Extra cost if only one pilot customer | **Known as a project decision** | — | KNOWN (source decision) |
| A-14 | Read-only access is sufficient for MVP value | Source; inference | Users demand actions | Medium | Interviews | TO VALIDATE |
| A-15 | Users prefer natural language over filters/forms for ad-hoc questions | Source vision | Simpler UI would suffice | Low | Prototype A/B | TO VALIDATE |

---

## 24. Validation Strategy

### 24.1 Methods

| Method | Validates | Feasibility for an individual contributor | Notes |
|---|---|---|---|
| Supply-chain practitioner / expert interviews | Problem, pain, users, workflow, evidence needs | Feasible (professional network) | Do **not** use confidential employer data |
| Analyst interviews | Workflow, time-on-task, tools | Feasible | Ask about last real investigation |
| Workflow observation (walkthrough) | Current workflow hypothesis | Partially feasible | Screen-share of a sanitised past case |
| Prototype testing (clickable or thin) | Trust, evidence format, NL preference | Feasible after Phase 9–12 | — |
| Synthetic-data experiments | Investigation correctness on seeded root causes | Highly feasible | Also becomes evaluation data |
| Public datasets (e.g. Olist) | Data Q&A realism | Feasible | Limited inventory/warehouse depth |
| Pilot users | Adoption, value | Later (post-MVP) | Requires security approval |
| Time-on-task measurement | V1–V4 | Feasible in prototype testing | Baseline first |
| Before/after workflow comparison | Business value | Pilot stage | — |
| Desk research on existing solutions | Differentiation | Feasible | Required before Phase 1 is locked |

### 24.2 Questions to answer before Phase 1

1. Which organization type is the beachhead?
2. Who is the primary user, and who is the economic buyer?
3. Which exception/investigation type is most painful and frequent?
4. How long does that investigation take today, and what tools are used?
5. What evidence would make a user trust an AI explanation?
6. Do existing BI/ERP/WMS tools (including their AI features) already solve the target questions?
7. Which data domains are essential for the first investigation?
8. Is tenant-specific knowledge (policies/SOPs) actually available and valuable?
9. What level of AI autonomy is acceptable (answer only, recommend, act)?
10. What single measurable outcome matters most to the buyer?

### 24.3 Pragmatic path given the learning-platform context

Because SupplyNexus AI is built by an individual contributor and doubles as a learning platform [SOURCE-DERIVED FACT], full customer discovery may not be possible before Phase 1. Two legitimate options exist:

| Option | Description | Trade-off |
|---|---|---|
| **1. Light validation sprint** (recommended) | 3–5 conversations with supply-chain practitioners + desk research on existing solutions before Phase 1 | Small delay; much stronger PRD |
| **2. Declared-assumption decision** | Owner explicitly accepts key assumptions (A-02, A-03, A-07) as working hypotheses, records this in ADR-0001, and proceeds | Faster; risk that the PRD optimises for an unvalidated problem |

---

## 25. Major Risks

> Likelihood is described qualitatively; no numerical likelihoods are claimed.

| # | Risk | Impact | Likelihood (qualitative) | Evidence | Mitigation hypothesis | Validation requirement |
|---|---|---|---|---|---|---|
| R1 | Problem not painful enough | Low adoption | Unknown | Pain magnitude UNKNOWN in source | Focus on *why*/exception questions | Interviews, time-on-task |
| R2 | Users do not trust AI answers | Low adoption | Plausible | General industry experience | Evidence with every answer; show interpretation; refuse when unsure | Prototype testing |
| R3 | Inaccurate answers (wrong numbers, wrong root causes) | Loss of trust; wrong decisions | Plausible | General engineering knowledge | Deterministic computation; validation; golden datasets | AI evaluation |
| R4 | Hallucination in knowledge answers | Wrong policy application | Plausible | General knowledge | Grounding + citations + "I don't know" | Groundedness evaluation |
| R5 | Poor data quality in customer systems | Wrong conclusions | Plausible | General knowledge | Surface data-quality issues as evidence | Pilot |
| R6 | Integration complexity | Slow onboarding; high cost | Likely for real customers | Many heterogeneous systems (source) | MVP on safe datasets; integrations later | Phase 7 |
| R7 | Security concerns block adoption | Sales blocked | Plausible | Security owner concerns | Governed, read-only, audited design | Security-owner conversations |
| R8 | Cross-tenant data leakage | Catastrophic trust loss | Low if designed; catastrophic impact | Source: never allow | Tenant isolation everywhere (Phase 6) | Security testing |
| R9 | Prompt injection / indirect injection via documents | Data exposure, wrong answers | Plausible | Source identifies it | Guardrails (Phase 6) | Adversarial evaluation |
| R10 | AI cost too high | Unviable pricing | Unknown | — | Deterministic-first routing; bounded investigations | Cost measurement |
| R11 | Latency too high for investigations | Poor UX | Plausible | Multi-step reasoning | Bounded steps; progressive results | Performance testing |
| R12 | Overly broad MVP | MVP never ships | **High** without scoping | Long technology list; 3 capabilities; individual builder | Section 17 boundary | Phase 1 scope review |
| R13 | Weak differentiation | No reason to buy | Plausible | Embedded AI in existing tools | D1+D2+D4 focus | Competitive research |
| R14 | Users prefer existing BI tools | Low adoption | Plausible | — | Complement, not replace, BI | Interviews |
| R15 | Governance/compliance requirements (AI governance, data residency) | Delays | Unknown | — | Auditability, evaluation evidence | Buyer conversations |
| R16 | Learning goals drive scope ("technology showcase" drift) | Complexity, no product focus | **Plausible** | Long list of AI concepts in source; source also warns against it | Problem → requirement → decision → ADR discipline | Phase reviews |
| R17 | Synthetic data makes demos unconvincing | Weak validation | Plausible | — | Realistic seeded scenarios reviewed by practitioners | Practitioner review |

---

## 26. Product Principles

| # | Principle | Basis |
|---|---|---|
| PP-1 | **Solve a real supply-chain problem before introducing AI.** | SOURCE-DERIVED |
| PP-2 | **Important answers carry evidence.** Users can see where an answer came from. | SOURCE-DERIVED |
| PP-3 | **AI augments people; humans own decisions.** | ARCHITECTURAL / PRODUCT RECOMMENDATION |
| PP-4 | **Deterministic logic stays deterministic.** AI interprets and explains; software computes, validates and enforces. | SOURCE-DERIVED |
| PP-5 | **Security and tenant isolation are foundational**, not features. | SOURCE-DERIVED |
| PP-6 | **The MVP stays focused**: one beachhead, one primary persona, one investigation type. | SOURCE-DERIVED (focus) + RECOMMENDATION (specifics) |
| PP-7 | **Every major capability has measurable value.** | SOURCE-DERIVED |
| PP-8 | **Complexity must be justified** by a problem, never by popularity. | SOURCE-DERIVED |
| PP-9 | **Enterprise trust is a product requirement.** | SOURCE-DERIVED |
| PP-10 | **AI is governed, never unrestricted.** Read-only first; actions require approval. | SOURCE-DERIVED |
| PP-11 | **Say "I don't know"** rather than guess. | ARCHITECTURAL / PRODUCT RECOMMENDATION |
| PP-12 | **Complement existing systems**; do not replace systems of record or BI. | ARCHITECTURAL / PRODUCT RECOMMENDATION |

---

## 27. Future Product Vision

> Everything in this section is **FUTURE VISION** and must **not** expand the MVP.

| Horizon | Capability | Description |
|---|---|---|
| **MVP** | Governed data Q&A; tenant knowledge Q&A; oversell investigation | Section 17 |
| Post-MVP 1 | More investigations | Order, warehouse backlog *why*, returns spikes |
| Post-MVP 1 | Real connectors | Governed connectors to WMS/ERP/OMS/TMS, databases, APIs, files, object storage |
| Post-MVP 2 | Domain intelligence | Inventory, order, warehouse, supplier, shipment, returns, procurement intelligence |
| Post-MVP 2 | Proactive detection | Anomaly and risk detection; exceptions surfaced before users ask |
| Post-MVP 3 | Recommendations | Evidence-backed recommended actions |
| Post-MVP 3 | Predictive & what-if | Forecasting, scenario analysis |
| Long-term | Controlled actions | Human-approved, audited write actions |

**Guardrail:** a future capability enters scope only through the same discipline: *problem → requirement → alternatives → trade-offs → decision → implementation → measurement → ADR*. [SOURCE-DERIVED FACT]

---

## 28. 30-Second Product Explanation

> SupplyNexus AI is a supply-chain intelligence assistant for operations and supply-chain analysts. When something goes wrong — an item gets oversold, a warehouse backs up, orders run late — the answer is usually spread across several systems, documents and people, and finding it takes hours of manual work. SupplyNexus lets analysts ask in plain language and get answers backed by evidence from their own data and policies, including *why* an exception happened. It is governed and read-only by design, so organizations can trust what it accesses and what it says.

---

## 29. 2-Minute Product Explanation

> **The problem.** Supply-chain operations run on many systems — order management, warehouse management, ERP, transportation — plus policies and procedures in documents. Dashboards tell you *what* is happening, but when a manager asks *why* SKU-10023 was oversold or why a warehouse is backed up, an analyst has to export data from several systems, reconcile it in spreadsheets, check policies and chase other teams. That investigation is slow, manual and depends on a few experts. *(This is our working hypothesis; we are validating how painful it is.)*
>
> **The product.** SupplyNexus AI is a multi-tenant SaaS assistant that answers three kinds of questions differently. Factual data questions — "how many orders were returned last month?" — are answered by governed, read-only data access with deterministic calculations, so the numbers are exact. Knowledge questions — "what is *our* safety-stock policy?" — are answered from the organization's own documents, with citations. And investigation questions — "why was this SKU oversold?" — are handled by a bounded investigation that gathers evidence step by step and explains the likely cause, showing the evidence behind it.
>
> **Why it's different.** It is not a generic chatbot: it answers from the customer's governed data and documents, shows its evidence, uses AI only where reasoning or language is needed, and never has unrestricted access to data.
>
> **The MVP.** One primary user (the analyst), one organization type, bounded data questions, a curated knowledge base, and one investigation type — overselling — on public and synthetic data.

---

## 30. 5-Minute Product Explanation

**1. Problem.** Supply-chain teams face a steady stream of operational exceptions: overselling, stockouts, inventory discrepancies, warehouse backlog, shipment delays, returns spikes. The information needed to explain them is fragmented across order, warehouse, inventory, ERP and transportation systems, and across documents and people. Existing dashboards are good at known KPIs but weak at ad-hoc and *why* questions. *(Hypothesis — magnitude to validate.)*

**2. Users.** Our primary user hypothesis is the supply-chain or operations analyst — the person who fields ad-hoc questions and investigates exceptions. The main consumers of their work are operations, inventory and warehouse managers. IT, data and security owners are essential stakeholders because they must approve access.

**3. Today's workflow.** Question arrives → decide which systems → open reports → export → combine in Excel or SQL → contact other teams → check policies → form hypotheses → prepare an explanation. Much of this is manual, repetitive and cross-system; the reasoning step is the only part that fundamentally needs human-like judgement.

**4. Product.** SupplyNexus AI is a natural-language assistant that first works out what kind of question it is facing. It does not treat everything as an AI problem.

**5. Data value.** For factual questions, AI only interprets the question; the actual answer comes from governed, read-only data access with deterministic calculations. The user sees how their question was interpreted so they can spot mistakes.

**6. Knowledge value.** For policy and procedure questions, answers come from the organization's own documents, with citations — not from general model knowledge.

**7. Investigation.** For *why* questions, SupplyNexus runs a bounded investigation: it gathers evidence through approved capabilities, correlates it, and explains the most likely cause with the evidence attached. In the MVP, this is limited to one exception type — overselling — so that we can measure whether it gets the right answer.

**8. Evidence.** Every important answer carries its evidence: records, calculations, document passages, timelines. Trust is treated as a product requirement.

**9. Business value.** Hypotheses: shorter investigation time, more analyst capacity, faster decisions, more consistent explanations. We have not measured any of these yet; we will baseline them.

**10. Governance.** Multi-tenant from day one, strict tenant isolation, read-only access, no arbitrary queries, auditing. These are what allow a security team to say yes.

**11. MVP.** One beachhead organization type, one primary persona, bounded data questions, a curated knowledge base, one investigation type, public and synthetic data.

**12. Future vision.** More investigation types, real system connectors, proactive anomaly detection, recommendations, predictive and what-if analysis, and eventually human-approved actions — each added only when a real problem justifies it.


---

## 31. Phase 0 Decision Summary

# PHASE 0 DECISION SUMMARY

| Item | Current position | Status |
|---|---|---|
| **Current product hypothesis** | A governed, evidence-backed supply-chain intelligence assistant that answers data, knowledge and investigation questions differently | SOURCE-DERIVED + INFERENCE |
| **Primary problem** | Slow, manual, cross-system investigation of operational questions and exceptions due to fragmented data and knowledge | INFERENCE — pain magnitude TO VALIDATE |
| **Primary user hypothesis** | Supply-chain / operations analyst | TO VALIDATE |
| **Primary consumer** | Operations / supply-chain manager | TO VALIDATE |
| **Target organization hypothesis** | Beachhead: omnichannel retailer or distributor operating its own warehouses | TARGET MARKET HYPOTHESIS |
| **Economic buyer** | Head of Supply Chain / Operations (hypothesis) | UNKNOWN |
| **Core use cases** | UC-1 tenant knowledge; UC-2 governed data Q&A; UC-3 oversell investigation | RECOMMENDATION |
| **MVP hypothesis** | Section 17 boundary — reduced from the original three open-ended capabilities to bounded versions | RECOMMENDATION — owner decision required |
| **Starting hypothesis verdict** | Keep all three capabilities, **bounded**; reframe Knowledge as tenant-specific; limit Investigation to one type; Data is foundational | RECOMMENDATION |
| **Non-goals** | Section 19 (NG-1 … NG-11) | SOURCE-DERIVED + RECOMMENDATION |
| **Differentiation hypotheses** | Strongest: investigation (D1) + evidence (D2) + supply-chain specificity (D4) | HYPOTHESIS — COMPETITIVE VALIDATION REQUIRED |
| **Business-value hypotheses** | V1–V9; no ROI claimed | UNKNOWN / TO VALIDATE |
| **Success metrics** | Defined across business, user, product, AI, operational categories; no targets yet | DEFINED — targets UNKNOWN |
| **Major assumptions** | A-02 (frequent *why* questions), A-03 (analyst is primary user), A-05 (evidence builds trust), A-07 (oversell is painful) | TO VALIDATE |
| **Validation requirements** | Section 24.2 ten questions | OPEN |
| **Major risks** | R12 broad MVP, R16 technology-showcase drift, R1 insufficient pain, R13 weak differentiation, R3/R4 inaccuracy, R8 tenant leakage | IDENTIFIED |
| **Open questions** | Section 32 | OPEN |

### Key product decisions proposed in this phase

| ID | Decision | Type |
|---|---|---|
| PD-01 | SupplyNexus AI is a **decision-support / investigation** product, not a system of execution | Proposed |
| PD-02 | The three capabilities are retained, but **bounded** for the MVP | Proposed |
| PD-03 | Knowledge capability is **tenant-specific and cited**, not generic definitions | Proposed |
| PD-04 | MVP includes **one** investigation type: inventory overselling | Proposed |
| PD-05 | AI interprets and explains; **deterministic software computes, authorizes and enforces** | Source-derived |
| PD-06 | **Read-only**, governed, tenant-scoped access in MVP | Source-derived |
| PD-07 | MVP uses **public / synthetic / safe data** only, likely including seeded synthetic inventory scenarios | Source-derived + Proposed |
| PD-08 | Product-value priority (Data → Investigation → Knowledge) is distinct from build order (current SDLC order retained) | Proposed |
| PD-09 | Single primary persona (analyst) and single beachhead organization type | Proposed — TO VALIDATE |

These decisions should be recorded in **ADR-0001 — Product Discovery** once the owner accepts or amends them. [SOURCE-DERIVED: the source anticipates `docs/decisions/ADR-0001-product-discovery.md`] ADR-0001 is **not** created by this document.

---

## 32. Open Questions

# OPEN QUESTIONS BEFORE PHASE 1

| # | Question | Why it blocks Phase 1 | Owner |
|---|---|---|---|
| OQ-01 | Who is the first target customer (beachhead organization type)? | Determines use cases, data domains and language | Product owner |
| OQ-02 | Who is the economic buyer? | Determines which outcome the PRD optimises | Product owner |
| OQ-03 | Which workflow/exception is most painful? Is overselling the right flagship investigation? | Determines the MVP investigation | Validation |
| OQ-04 | Which capability should the MVP *lead* with in positioning? | Affects PRD priorities | Product owner |
| OQ-05 | Which data sources/domains are essential for the first investigation? | Drives data requirements (Phase 1 → Phase 4) | Product owner + validation |
| OQ-06 | What evidence do users require to trust an answer? | Defines answer/evidence requirements | Validation |
| OQ-07 | What level of AI autonomy is acceptable (answer / recommend / act)? | Defines boundaries & human-in-the-loop requirements | Validation |
| OQ-08 | What single measurable business outcome matters most? | Defines primary success metric | Buyer validation |
| OQ-09 | Is tenant-specific knowledge available and valuable in target organizations? | Determines Knowledge capability value | Validation |
| OQ-10 | Do existing tools (including embedded AI features) already solve the target questions? | Determines differentiation | Desk research |
| OQ-11 | Will a light validation sprint be done, or will assumptions be formally accepted (Section 24.3)? | Determines Phase 0 exit | Product owner |
| OQ-12 | Accept, amend or reject the proposed MVP boundary (Section 17) and decisions PD-01 … PD-09? | Required input to PRD | Product owner |

---

## 33. Phase 1 Inputs

# PHASE 1 — REQUIREMENTS & PRD INPUTS

> Phase 1 is **not** performed here. This section only lists what Phase 1 must receive.

| # | Input | Source section |
|---|---|---|
| 1 | Product vision and working thesis | §2, §3 |
| 2 | Problem statement (with confidence and validation needs) | §4, §5 |
| 3 | Beachhead organization hypothesis | §6 |
| 4 | Primary persona and secondary personas | §7, §8 |
| 5 | Buyer / stakeholder map, including security & IT concerns | §9 |
| 6 | Prioritised pain points | §10 |
| 7 | Current workflow hypothesis | §11 |
| 8 | Existing-solution landscape (with competitive validation gap) | §12 |
| 9 | AI vs non-AI boundary | §13, §14 |
| 10 | Core use cases UC-1, UC-2, UC-3 (+ candidates) | §15 |
| 11 | Proposed MVP boundary and rationale | §16, §17 |
| 12 | Product scope and non-goals | §18, §19 |
| 13 | Differentiation and business-value hypotheses | §20, §21 |
| 14 | Success metric definitions (targets to be set) | §22 |
| 15 | Assumptions register | §23 |
| 16 | Validation questions and chosen validation path | §24 |
| 17 | Risk register | §25 |
| 18 | Product principles | §26 |
| 19 | Proposed product decisions PD-01 … PD-09 (owner-accepted version) | §31 |
| 20 | Resolved or explicitly accepted open questions | §32 |

---

## Phase 0 Final Quality Check

| Check | Result |
|---|---|
| Product problem is clear | ✓ (magnitude TO VALIDATE) |
| Target user clear or marked TO VALIDATE | ✓ TO VALIDATE |
| Target organization clear or marked TO VALIDATE | ✓ TARGET MARKET HYPOTHESIS |
| Buyer identified or marked UNKNOWN | ✓ UNKNOWN (hypothesis stated) |
| Business value defined as hypotheses | ✓ |
| Core use cases identified | ✓ |
| AI opportunities justified | ✓ |
| Non-AI opportunities identified | ✓ |
| MVP bounded | ✓ (proposed) |
| Non-goals explicit | ✓ |
| Differentiation treated as hypothesis | ✓ |
| Success metrics exist | ✓ (no targets) |
| Assumptions documented | ✓ |
| Validation questions exist | ✓ |
| Risks documented | ✓ |
| Future vision separated from MVP | ✓ |
| 30-second / 2-minute / 5-minute explanations | ✓ |
| Phase 1 inputs identified | ✓ |
| No application code, schema, API or deployment design | ✓ |
| No unsupported customer claims presented as facts | ✓ |

---

## Phase 0 Status

**PHASE 0 STATUS: CONTINUE PRODUCT DISCOVERY**

**Why not "Ready for Phase 1":** the document is complete as a *discovery artifact*, but the source's own completion rule says Phase 0 must not be declared complete simply because documents exist. Three items that the PRD depends on remain unresolved:

1. **Buyer** is UNKNOWN (OQ-02).
2. **Pain magnitude** and the **flagship investigation** (overselling) are unvalidated (OQ-03).
3. The **proposed MVP boundary and product decisions** have not yet been accepted by the product owner (OQ-12).

**What moves Phase 0 to READY FOR PHASE 1:**

- The owner accepts/amends the MVP boundary and PD-01 … PD-09; **and**
- Either (a) a light validation sprint (3–5 practitioner conversations + desk research) is completed, **or** (b) the owner formally accepts the key assumptions (A-02, A-03, A-05, A-07) as working hypotheses and records this in ADR-0001.

Either route is legitimate for a learning-platform project; route (b) must be explicit so that the PRD is honest about what is unvalidated.

---

# SUPPLYNEXUS AI — PHASE 0 HANDOFF

## CURRENT STATE

Phase 0 discovery document v0.1 drafted. No code, repository structure, architecture or technology decisions exist. Status: **CONTINUE PRODUCT DISCOVERY** pending owner decisions (OQ-11, OQ-12) and buyer/pain validation.

## WHAT WE DISCOVERED

- The source thesis (fragmented data and knowledge make investigation hard) is plausible but unvalidated.
- The strongest AI value lies in **investigation** and **tenant-specific knowledge**; counts, rankings and KPIs should be deterministic.
- Generic knowledge ("What is safety stock?") is weakly differentiated; tenant-specific, cited knowledge is the useful version.
- NL data querying alone risks commoditisation by AI features in existing tools.
- The original three-capability MVP is directionally right but **too broad** unless each capability is bounded.
- Investigation depends on governed data access; product-value order and build order are different and both valid.
- Public data (Olist) likely lacks the inventory depth needed for oversell investigation; seeded synthetic data is likely required and doubles as evaluation data.

## PRODUCT VISION

A governed, evidence-backed supply-chain intelligence assistant that helps teams understand, query and investigate their operations — evolving from knowledge and data answers, to investigations, to recommendations and, eventually, approved actions.

## PROBLEM STATEMENT

Supply-chain and operations teams cannot quickly obtain trustworthy answers and explanations about their operation because data and knowledge are spread across systems, documents and people; explaining *why* an exception occurred is especially slow and manual. (Magnitude TO VALIDATE.)

## TARGET USERS

Primary: supply-chain / operations analyst (TO VALIDATE). Secondary: operations/supply-chain, inventory and warehouse managers. Occasional: logistics, procurement, executives. Technical/administrative: data analysts, IT/data team, tenant admins.

## TARGET ORGANIZATIONS

TARGET MARKET HYPOTHESIS — beachhead: omnichannel retailer or distributor operating its own warehouses. Others (3PLs, manufacturers, logistics, warehouse operators) are later candidates.

## PERSONAS

Priya (Analyst — primary), Daniel (Ops/SC Manager — primary consumer), Marcus (Inventory Manager), Elena (Warehouse Manager), Sam (IT/Data Owner), Executive/Director. All hypothetical.

## BUYERS / STAKEHOLDERS

Economic buyer UNKNOWN (hypothesis: Head of Supply Chain/Operations). Critical gatekeepers: security owner, IT owner, data owner, AI governance owner.

## CORE USE CASES

UC-1 Tenant supply-chain knowledge (cited) · UC-2 Governed NL data query (bounded families) · UC-3 Inventory oversell investigation (flagship). Candidates: order, warehouse *why*, returns investigation.

## AI VS NON-AI USE CASES

AI: knowledge synthesis, NL intent interpretation, investigation reasoning/correlation, summarisation. Non-AI: calculations, filtering/ranking, authentication, authorization, tenant isolation, validation, business rules, known-exception detection, query safety, audit.

## MVP

Analyst at one beachhead; governed read-only data Q&A for orders/returns/inventory/warehouse backlog; cited tenant knowledge Q&A over a curated corpus; one investigation type (overselling) with evidence; multi-tenant; public/synthetic data only.

## NON-GOALS

No ERP/WMS/OMS/TMS replacement; no autonomous execution; no unrestricted data access; no autonomous decisions; no generic chatbot; not every domain; no forecasting yet; no technology showcase; no BI replacement; no AI arithmetic; no cross-tenant sharing.

## PRODUCT DIFFERENTIATION HYPOTHESES

Strongest: investigation (D1) + evidence (D2) + supply-chain specificity (D4). Governance and tenancy are likely table-stakes. COMPETITIVE VALIDATION REQUIRED.

## BUSINESS VALUE

Hypotheses V1–V9 (investigation time, analyst productivity, decision speed, reduced manual reporting, faster root cause, reduced search effort, visibility, consistency, reduced shadow-AI). No ROI claimed.

## SUCCESS METRICS

Business, user, product, AI and operational metrics defined; targets to be set after baselining. Zero cross-tenant leakage is a requirement.

## ASSUMPTIONS

A-01 … A-15; most critical: A-02, A-03, A-05, A-07, A-04 (buyer).

## VALIDATION ITEMS

Section 24.2 (ten questions); choose validation sprint or declared-assumption route.

## RISKS

Most significant: broad MVP (R12), technology-showcase drift (R16), insufficient pain (R1), weak differentiation (R13), inaccurate answers (R3/R4), tenant leakage (R8), prompt injection (R9), cost/latency (R10/R11).

## PRODUCT PRINCIPLES

PP-1 … PP-12 (problem before AI; evidence; humans decide; deterministic stays deterministic; security & tenancy foundational; focused MVP; measurable value; justified complexity; trust; governed AI; say "I don't know"; complement existing systems).

## KEY PRODUCT DECISIONS

PD-01 … PD-09 (proposed; PD-05, PD-06 are source-derived). To be confirmed by the owner and recorded in ADR-0001.

## OPEN QUESTIONS

OQ-01 … OQ-12, most critically OQ-02 (buyer), OQ-03 (flagship pain), OQ-11 (validation route), OQ-12 (accept MVP boundary).

## PHASE 1 INPUTS

Section 33 — twenty inputs, including the owner-accepted MVP boundary and decisions.

## NEXT OBJECTIVE

Close OQ-11 and OQ-12; complete light validation or record accepted assumptions in ADR-0001; then transition Phase 0 to READY FOR PHASE 1.

**NEXT PHASE: PHASE 1 — REQUIREMENTS & PRD** (not started)
