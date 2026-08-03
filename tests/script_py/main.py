from pathlib import Path
import json
import requests


def main():
    print("Hello from script-py!")

    output_dir = Path("output")
    output_dir.mkdir(exist_ok=True)

    base_url = "http://localhost:8765"
    task_url = f"{base_url}/tasks"

    for i in range(10000):
        req = requests.post(
            task_url,
            json={
                "title": "Nova tarefa"
            }
        )
        req.raise_for_status()

        data = req.json()

        total_files = len(list(output_dir.iterdir()))
        response_path = output_dir / f"response_{total_files}.json"

        with response_path.open("w", encoding="utf-8") as f:
            json.dump(data, f, ensure_ascii=False, indent=4)

        print(f"Loop {i} | Salvo em {response_path}")

    total_files = len(list(output_dir.iterdir()))
    print(f"{total_files} Arquivos totais em {output_dir}")


if __name__ == "__main__":
    main()