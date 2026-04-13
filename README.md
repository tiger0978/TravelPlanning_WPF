# TravelPlanning_WPF 🗺️ (WIP)

本專案是一個基於 Windows Presentation Foundation (WPF) 開發的桌面端旅遊規劃應用程式。主要靈感來自於知名旅遊規劃 App「Funliday」，旨在提供使用者一個直覺、流暢的旅遊行程排定與地圖互動體驗。

目前專案處於開發階段 (Work In Progress)，已具備核心的地圖整合、景點搜尋、路線規劃與清單儲存等功能。

## ✨ 核心功能 (Features)

* **🗺️ 互動式地圖體驗**：整合自建的 Google API SDK 與 GMap SDK，提供流暢的地圖瀏覽、平移與縮放功能。
* **🔍 景點搜尋與自動完成**：支援關鍵字搜尋景點，並提供 AutoComplete 自動完成建議。
* **❤️ 最愛景點與清單管理**：可將感興趣的景點加入收藏清單 (Save List)，方便後續排入行程。
* **📅 行程與路線規劃**：建立多天數的旅遊計畫，支援在地圖上直接將景點加入每日行程，並進行路線規劃與預覽。
* **🗂️ 現代化 UI 介面**：套用 [WPF-UI](https://github.com/lepoco/wpfui) 模板，提供符合 Windows 11 風格的現代化視覺與流暢動畫體驗。

## 🛠️ 技術架構 (Tech Stack & Architecture)

本專案採用 **MVP (Model-View-Presenter)** 與 **MVVM (Model-View-ViewModel)** 混合架構設計，並結合 **Repository Pattern** 進行資料持久化管理，以確保程式碼的高內聚與低耦合。

* **前端框架**: C# WPF (.NET)
* **UI 元件庫**: WPF-UI
* **架構模式**: MVP (邏輯控制) + MVVM (資料綁定)
* **資料存取**: Repository Pattern + DAO (Data Access Object) + Entity Framework (預期)
* **組件溝通**: Message / Event Bus (用於跨 ViewModel/Presenter 溝通，如 `PlaceSelectedMessage`)
* **外部服務**: 自建 Google API SDK, GMap SDK

### 📐 系統架構圖 (System Architecture)

```mermaid
graph TD
    subgraph UI Layer (Views / Components)
        V[WPF Views / XAML] --> |Data Binding| VM(ViewModels / Contexts)
        V --> |User Action| P(Presenters)
    end

    subgraph Logic Layer
        P --> |Update State| VM
        P --> |Publish/Subscribe| M((Message Bus))
        M -.-> |Notify| OtherP[Other Presenters/Components]
    end

    subgraph Data Access Layer
        P --> |Call| C[Contracts / Interfaces]
        C --> R[Repositories]
        R --> DAO[DAOs]
    end

    subgraph Infrastructure
        DAO --> DB[(Local Database)]
        R --> Ext[External SDKs\nGoogle API / GMap]
    end

<img width="865" height="343" alt="image" src="https://github.com/user-attachments/assets/960d0112-fc66-4f5f-9007-349b8f37b6a5" />

<img width="865" height="375" alt="image" src="https://github.com/user-attachments/assets/2195c1ee-c090-49c0-bac5-4e7efd6c6349" />




