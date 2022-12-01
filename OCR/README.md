
*this is my "Introduction to Information and Communication Technology" report*

# 0. Lời nói đầu - OCR

> Nhận dạng ký tự quang học *(tiếng Anh: Optical Character Recognition, viết tắt là OCR)*, là loại phần mềm máy tính được tạo ra để chuyển các hình ảnh của chữ viết tay hoặc chữ đánh máy (thường được quét bằng máy scanner) thành các văn bản tài liệu. OCR được hình thành từ một lĩnh vực nghiên cứu về nhận dạng mẫu, trí tuệ nhận tạo và machine vision. Mặc dù công việc nghiên cứu học thuật vẫn tiếp tục, một phần công việc của OCR đã chuyển sang ứng dụng trong thực tế với các kỹ thuật đã được chứng minh.

\- Nguồn: [wikipedia](https://vi.wikipedia.org/wiki/Nh%E1%BA%ADn_d%E1%BA%A1ng_k%C3%BD_t%E1%BB%B1_quang_h%E1%BB%8Dc)

Tính đến nay, người ta đã nghiên cứu ra rất nhiều cách để để phát triển một chương trình OCR. Nhóm **Flaming Watermelon** chúng em đã nghiên cứu được một cách được nhiều phần mềm OCR tin dùng. Trong bài viết này, chúng em sẽ trình bày đại khái ý tưởng của cách làm ấy.

# 1. Convolution

Trước khi nói về thuật toán, ta đến với một phép toán sẽ được đề cập rất nhiều trong bài viết này - **Convolution**

## 1.0. Hình dung

Convolution là phép toán thường được dùng trên *2 mảng k-chiều*, được ký hiệu bởi dấu $\ast$. Nói đại khái, gọi:

- $M_{x}$ là một điểm trong *không gian k-chiều* của mảng $M$
- $M_{f(n, x)}$ là các điểm lân cận $M_{x}$, có [Khoảng Cách Manhattan](https://vi.wikipedia.org/wiki/Kho%E1%BA%A3ng_c%C3%A1ch_Manhattan) với $M_{x}$ bé hơn hoặc bằng $n$ đơn vị.
- $F, G$ và $H$ là *3 mảng k-chiều*, trong đó, kích thước của $|G| = n^k$

Ta có giá trị của $H_{x}$ trong phép toán $H = F \ast G$ bằng:

$$\sum(F_{f(n, x)} \times G_{f(n, x)})$$

## 1.1. Video ví dụ

Lấy ví dụ, phép toán $a \ast b$ giữa $a = \{1, 2, 3\}$ và $b = \{6, 5, 4\}$ được tính như sau:

[o1.webm](https://user-images.githubusercontent.com/58514512/205090479-5c47d1a2-d939-467f-a135-06b2c87e41bd.webm)

\- Nguồn: [3Blue1Brown](https://www.youtube.com/watch?v=KuXjwB4LzSA)

Ta áp dụng tương tự với 2 mảng có độ dài lớn hơn:

[o.webm](https://user-images.githubusercontent.com/58514512/205090748-dd941980-2479-4ce4-8fd1-de183633d7b4.webm)

\- Nguồn: [3Blue1Brown](https://www.youtube.com/watch?v=KuXjwB4LzSA)

## 1.2. Khái quát hóa

2 ví dụ trên là minh họa cho phép toán $H = F \ast G$ (với $H, F, G$ là *3 mảng 1 chiều*). Ta có:

$$H_{x} = \sum_{i=x-k}^{i=x+k}(F_{i} * G_{i})$$

Tương tự, giá trị của $H_{yx}$ trong phép tính $H = F \ast G$ (với $H, F, G$ là *3 mảng 2 chiều* có cùng vai trò như trên) được tính như sau:

$$H_{yx} = \sum_{i=y-k}^{i=y+k}\sum_{j=x-k}^{j=x+k}(F_{ij} \times G_{ij})$$

Minh họa với $|F| = 6 \times 6$ và $|G| = 3 \times 3$:

![wikipedia](https://upload.wikimedia.org/wikipedia/commons/1/19/2D_Convolution_Animation.gif)

Trong Computer Vision, vai trò của $H, F, G$ trong phép toán $H = F \ast G$ lần lượt được gọi là:

- Mảng đích
- Mảng nguồn
- Kernel

## 1.3. Ý nghĩa của Convolution

Bằng việc tính tổng có trọng số của các điểm lân cận trong không gian, Convolution cho phép chúng ta biết được tính chất của một khu vực trong một bức ảnh kỹ thuật số (vốn là một mảng 2 chiều), đồng thời triển khai những phép biến đổi trên nó. 

Ứng dụng phổ biến nhất của Convolution có thể kể đến là Blur ảnh (làm mờ ảnh bằng cách loang các pixel lại với nhau). Trong thị giác máy tính, Convolution có vô số công dụng, trong đó bao gồm phát hiện độ biến thiên màu sắc (tính gradient), và phát hiện cạnh (Edge Detection).

# 2. Edge Detection

Các chữ cái trong một văn bản thường được viết rõ nét. Bằng việc sử dụng Convolution với một kernel có cấu trúc như sau:


| -1 | 0  | +1 |
| -- | -- | -- |
| -2 | 0  | +2 |
| -1 | 0  | +1 |

Ta có thể lấy "nét" của những chữ cái ấy, mà thực chất là những sự biến thiên màu sắc đột ngột. Kết quả như sau:

Source                     | Edge Detection             | Visualization
:-------------------------:|:-------------------------: | :-------------------------:
![src](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-src.jpg) | ![des](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-des.png) | ![visual](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-visual.png)
