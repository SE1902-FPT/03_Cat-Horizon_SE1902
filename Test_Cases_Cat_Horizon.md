# Kịch bản Kiểm thử - Cat Horizon (Setting & Instruction Scenes)

## 1. Màn hình Cài đặt (Setting Scene)

| ID | Test Case | Tình trạng Mô tả (Tiền điều kiện) | Các bước thực hiện (Steps) | Kết quả mong đợi (Expected Result) | Trạng thái (Pass/Fail) |
|---|---|---|---|---|---|
| SET_01 | Kiểm tra giao diện chung | Người chơi ở Scene Setting | Quan sát các thành phần hiển thị trên màn hình | Giao diện phải đồng bộ với art style "*Cat Horizon*", bố cục có Music Slider, Sound Slider và Back Button hiện đầy đủ, không bị lệch hoặc lỗi hiển thị. | |
| SET_02 | Kiểm tra lưu thiết lập (PlayerPrefs) | Game khởi chạy lần đầu tiên | Kéo thanh *Music* và *Sound* > Quay lại *Main Menu* > Thoát Game > Vào lại *Setting Scene* | Thanh trượt Music và Sound phải nằm đúng vị trí ở mức người thiết lập (vd: 50% hoặc 0%), không bị Reset về mặc định. | |
| SET_03 | Kiểm tra chức năng *Music Volume* | Người chơi ở Scene Setting | Thử kéo thanh *Music Slider* về mốc 0 (mute), sau đó chuyển Scene khác để kiểm tra | Nhạc nền (BGM) sẽ nhỏ dần và tắt khi ở mức 0, giữ trạng thái khi đổi Scene. (Test thông qua AudioMixer) | |
| SET_04 | Kiểm tra chức năng *Sound Volume* | Người chơi ở Scene Setting | Thử kéo thanh *Sound Slider* về mức tối đa, rồi thực hiện một hành động (như click button) phát âm thanh | Hiệu ứng âm thanh (SFX) kêu chính xác với mức âm lượng đã chỉnh. Không bị méo tiếng hoặc vỡ âm khi ở mức to nhất. | |
| SET_05 | Nút Quay Lại (Back Button) | Người chơi ở Scene Setting | Bấm chuột vào nút *Back* | Chuyển cảnh ngay lập tức qua Scene `MainMenu`. Không bị đứng game hoặc báo lỗi ở Console. | |

---

## 2. Màn hình Hướng dẫn (Instruction Scene)

| ID | Test Case | Tình trạng Mô tả (Tiền điều kiện) | Các bước thực hiện (Steps) | Kết quả mong đợi (Expected Result) | Trạng thái (Pass/Fail) |
|---|---|---|---|---|---|
| INS_01 | Kiểm tra hiển thị hình ảnh minh họa | Mở Scene Instructions | Quan sát các Sprite/Image về Player, Enemy, Boss, Keys trên màn hình | Ảnh phải hiển thị rõ, đúng kích thước (aspect ratio) được thiết lập, chữ không bị mờ hay nhòe. | |
| INS_02 | Kiểm tra nội dung Text (Luật chơi, Cách di chuyển) | Mở Scene Instructions | Đọc toàn bộ nội dung hướng dẫn cách điều khiển, cách bắn laser, tiêu diệt alien | Text phải hiển thị đúng font chữ của *Cat Horizon*, không bị lỗi font Tiếng Việt (nếu có), các lời hướng dẫn sắp xếp dễ đọc, logic. | |
| INS_03 | Nút Quay Lại (Back Button) | Mở Scene Instructions | Bấm chuột vào nút *Back* trên UI | Unity lập tức gọi hàm `SceneManager.LoadScene("MainMenu")`, chuyển màn hình mượt mà không bị lỗi thiếu Scene. | |
| INS_04 | Kiểm tra độ phản hồi cho nhiều độ phân giải màn hình | Người chơi ở Scene Instructions | Đổi Aspect Ratio trong Game View (16:9, 4:3, thiết bị di động nếu có) | Các Sprite minh họa, Text Canvas linh hoạt thay đổi kích thước mà không bị lệch hay che lấp lên nhau (Nhờ *Canvas Scaler*). | |

---

## 3. Quản lý Hệ thống Cảnh (Scene Management)

| ID | Test Case | Tình trạng Mô tả (Tiền điều kiện) | Các bước thực hiện (Steps) | Kết quả mong đợi (Expected Result) | Trạng thái (Pass/Fail) |
|---|---|---|---|---|---|
| SCM_01 | Kiểm tra đăng ký Build Settings | Unity Editor đang bật | File > Build Settings, kiểm tra danh sách "Scenes In Build" | `MainMenu`, `Setting`, `Instruction` hiển thị tích (tick). Đảm bảo Index của `MainMenu` sẵn sàng để chuyển tới. | |
| SCM_02 | Truy cập từ Main Menu | Đang ở Scene `MainMenu` | Nhấn nút *Setting* hoặc *Instruction* | Giao diện phải nhảy qua đúng Scene tương ứng, không có thông báo "*Scene 'XYZ' couldn't be loaded*" ở Console log. | |
