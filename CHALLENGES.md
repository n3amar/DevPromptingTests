# Dev Assessment Challenges

This repo is a small ASP.NET Core Web API that manages projects and tasks.
Before starting the challenges, spend a few minutes understanding the codebase.

---

## Challenge 1 — Extract a Service Layer

**File:** `DevTestRepo.Api/Controllers/ProjectsController.cs`

The controller is doing too much. Business logic — like calculating summaries,
enforcing rules, and orchestrating data access — belongs in a dedicated service,
not directly in the controller.

**Your task:**
- Create `DevTestRepo.Api/Services/ProjectService.cs`
- Move all non-trivial logic out of `ProjectsController` into the service
- Inject the service into the controller via the constructor
- Register the service in `Program.cs`

**Done when:**
- The controller methods contain only: receive input → call service → return result
- The service handles all business logic and data access
- The app builds and all endpoints still work

---

## Challenge 2 — Fix the N+1 Query

**File:** `DevTestRepo.Api/Controllers/TasksController.cs`, `GetAll` method

The `GET /api/tasks` endpoint has a performance problem. It hits the database
once per task to look up the project name, instead of joining in a single query.

**Your task:**
- Identify the N+1 query
- Rewrite `GetAll` to fetch all the data it needs in a single database round-trip
- The response shape should remain the same

**Done when:**
- `GET /api/tasks` makes exactly one database call regardless of how many tasks exist

---

## Challenge 3 — Find and Fix the Bug

**File:** `DevTestRepo.Api/Controllers/TasksController.cs` — `GetByProject` method

The `GET /api/tasks/by-project/{projectId}` endpoint has a logic bug. It consistently returns more tasks than it should.

**Your task:**
- Identify what the endpoint is supposed to do based on its name and route
- Find the bug in the LINQ query and fix it

**Done when:**
- `GET /api/tasks/by-project/1` returns only tasks belonging to project 1
- `GET /api/tasks/by-project/2` returns only tasks belonging to project 2

---

## Challenge 4 — Add Pagination to GetAll Projects

**File:** `DevTestRepo.Api/Controllers/ProjectsController.cs`, `GetAll` method

The `GET /api/projects` endpoint returns every project with no limit.
In production this would be a problem as data grows.

**Your task:**
- Add `page` and `pageSize` query parameters (e.g. `?page=1&pageSize=10`)
- Default `page` to 1 and `pageSize` to 10 if not provided
- Return the paginated results along with metadata: `totalCount`, `page`, `pageSize`, `totalPages`

**Done when:**
- `GET /api/projects?page=1&pageSize=2` returns 2 projects and correct metadata
- `GET /api/projects` still works with defaults
- `pageSize` is capped at 50 to prevent abuse

---

## Challenge 5 — Write xUnit Tests

**File:** `DevTestRepo.Tests/TaskServiceTests.cs`

There are no tests. After completing Challenge 1, the service layer should be
testable in isolation.

**Your task:**
- Write at least 3 xUnit tests covering the `ProjectService`
- Use an in-memory SQLite database (not mocks) so tests exercise real EF queries
- Cover at least: happy path, not-found case, and one business rule

**Done when:**
- `dotnet test` passes with at least 3 tests
- Tests do not depend on the running API or a file-based database
