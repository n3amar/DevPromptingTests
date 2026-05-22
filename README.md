# Dev Assessment — LLM Prompting & .NET Core Skills

This repo is a take-home assessment for web developers. It contains a small but realistic ASP.NET Core Web API with intentional gaps and bugs. Your job is to work through the challenges below.

There are no trick questions about HTTP status codes or framework internals. The challenges reflect the kind of work you'd do on a real PR.

---

## Getting Started

**Prerequisites**
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Any IDE or editor (Visual Studio, VS Code, Rider)

**Run the API**

```bash
git clone https://github.com/n3amar/DevPromptingTests.git
cd DevPromptingTests/DevTestRepo.Api
dotnet run
```

The API starts on `http://localhost:5226`. The SQLite database is created and seeded automatically on first run — no setup required.

**Run the tests**

```bash
cd DevPromptingTests/DevTestRepo.Tests
dotnet test
```

---

## What Does This Repo Do?

Before starting the challenges, take a few minutes to understand the codebase. It manages **projects** and **tasks** — think of a lightweight internal project tracker.

Ask your LLM assistant to explain the repo to you. A good first prompt might be:

> "Read through these files and explain what this API does, what endpoints it exposes, and what the data model looks like."

This is part of the assessment. We want to see how you use AI tooling to orient yourself in an unfamiliar codebase.

---

## The Challenges

There are 5 challenges. Complete as many as you can. Each has a clear **Done when** section so you know exactly what passing looks like.

---

### Challenge 1 — Extract a Service Layer

**File:** `DevTestRepo.Api/Controllers/ProjectsController.cs`

The controller is doing too much. Business logic — like calculating summaries, enforcing rules, and orchestrating data access — belongs in a dedicated service, not directly in the controller.

**Your task:**
- Create `DevTestRepo.Api/Services/ProjectService.cs`
- Move all non-trivial logic out of `ProjectsController` into the service
- Inject the service into the controller via the constructor
- Register the service in `Program.cs`

**Done when:**
- Controller methods contain only: receive input → call service → return result
- The service handles all business logic and data access
- The app builds and all endpoints still work

---

### Challenge 2 — Fix the N+1 Query

**File:** `DevTestRepo.Api/Controllers/TasksController.cs` — `GetAll` method

The `GET /api/tasks` endpoint has a performance problem. It hits the database once per task to look up the project name, instead of fetching everything in a single query.

**Your task:**
- Identify the N+1 query
- Rewrite `GetAll` to fetch all the data it needs in a single database round-trip
- The response shape should remain the same

**Done when:**
- `GET /api/tasks` makes exactly one database call regardless of how many tasks exist

---

### Challenge 3 — Find and Fix the Bug

**File:** `DevTestRepo.Api/Controllers/TasksController.cs` — `GetByProject` method

The `GET /api/tasks/by-project/{projectId}` endpoint has a logic bug. It consistently returns more tasks than it should.

**Your task:**
- Identify what the endpoint is supposed to do based on its name and route
- Find the bug in the LINQ query and fix it

**Done when:**
- `GET /api/tasks/by-project/1` returns only tasks belonging to project 1
- `GET /api/tasks/by-project/2` returns only tasks belonging to project 2

---

### Challenge 4 — Add Pagination to GetAll Projects

**File:** `DevTestRepo.Api/Controllers/ProjectsController.cs` — `GetAll` method

The `GET /api/projects` endpoint returns every project with no limit. In production this becomes a problem as data grows.

**Your task:**
- Add `page` and `pageSize` query parameters (e.g. `?page=1&pageSize=10`)
- Default `page` to `1` and `pageSize` to `10` if not provided
- Return the paginated results along with metadata: `totalCount`, `page`, `pageSize`, `totalPages`
- Cap `pageSize` at `50` to prevent abuse

**Done when:**
- `GET /api/projects?page=1&pageSize=2` returns 2 projects and correct metadata
- `GET /api/projects` still works using the defaults

---

### Challenge 5 — Write xUnit Tests

**File:** `DevTestRepo.Tests/TaskServiceTests.cs`

There are no tests. After completing Challenge 1, the service layer should be testable in isolation.

**Your task:**
- Write at least 3 xUnit tests covering `ProjectService`
- Use an in-memory SQLite database — not mocks — so tests exercise real EF queries
- Cover at least: a happy path, a not-found case, and one business rule

**Done when:**
- `dotnet test` passes with at least 3 tests
- Tests do not depend on the running API or a file-based database

---

## Final Step — LLM Retrospective

Before submitting, complete the retrospective in `RETROSPECTIVE_PROMPT.md`.

1. Open the LLM chat you used during the assessment — **do not start a new conversation**
2. Copy the prompt from `RETROSPECTIVE_PROMPT.md` and paste it at the end of your session
3. Save the output as `RETROSPECTIVE.md` in the root of your fork
4. Commit and push it with your code changes

The retrospective output also includes two personal artifacts generated from your session — a set of CLAUDE.md rules and a prompting checklist skill tailored to how you specifically worked today. These are yours to keep and use going forward. They are not assessed, but they are a useful signal of self-awareness if you choose to include them.

---

## Submitting Your Work

Push your changes to a fork of this repo and share the link. Make sure the fork is public and includes your `RETROSPECTIVE.md`.

We'll review:
- Whether your code is correct and fits the existing patterns
- Code quality — not just "does it compile"
- Your `RETROSPECTIVE.md` — how well you used your LLM assistant, what context you gave it, and how critically you evaluated its output

There's no time limit, but most candidates complete this in 2–3 hours.
