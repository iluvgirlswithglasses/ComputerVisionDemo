
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

Hai ví dụ trên là minh họa cho phép toán $H = F \ast G$ (với $H, F, G$ là *3 mảng 1 chiều*). Ta có:

$$H_{x} = \sum_{i=x-k}^{i=x+k}(F_{i} * G_{i})$$

Tương tự, giá trị của $H_{yx}$ trong phép tính $H = F \ast G$ (với $H, F, G$ là *3 mảng 2 chiều* có cùng vai trò như trên) được tính như sau:

$$H_{yx} = \sum_{i=y-k}^{i=y+k}\sum_{j=x-k}^{j=x+k}(F_{ij} \times G_{ij})$$

Minh họa với $|F| = 6 \times 6$ và $|G| = 3 \times 3$:

![wikipedia](https://upload.wikimedia.org/wikipedia/commons/1/19/2D_Convolution_Animation.gif)

\- Nguồn ảnh: [wikipedia](https://upload.wikimedia.org/wikipedia/commons/1/19/2D_Convolution_Animation.gif)

Trong Computer Vision, vai trò của $H, F, G$ trong phép toán $H = F \ast G$ lần lượt được gọi là:

- Mảng đích
- Mảng nguồn
- Kernel

## 1.3. Ý nghĩa của Convolution

Bằng việc tính tổng có trọng số của các điểm lân cận trong không gian, Convolution cho phép chúng ta biết được tính chất của một khu vực trong một bức ảnh kỹ thuật số (vốn là một mảng 2 chiều), đồng thời triển khai những phép biến đổi trên nó. 

Ứng dụng phổ biến nhất của Convolution có thể kể đến là Blur ảnh (làm mờ ảnh bằng cách loang các pixel lại với nhau). Trong thị giác máy tính, Convolution có vô số công dụng, trong đó bao gồm phát hiện độ biến thiên màu sắc (tính gradient), và phát hiện cạnh (Edge Detection).

# 2. Sobel Operator

## 2.0. Ứng dụng cơ bản của Sobel Operator

Các chữ cái trong một văn bản thường được viết rõ nét. Bằng việc sử dụng Convolution với một kernel có cấu trúc như sau:


| -1 | 0  | +1 |
| -- | -- | -- |
| -2 | 0  | +2 |
| -1 | 0  | +1 |

Ta có thể lấy "nét" của những chữ cái ấy, mà thực chất là những sự biến thiên màu sắc đột ngột. Kết quả như sau:

Source                     | Edge Detection             | Visualization
:-------------------------:|:-------------------------: | :-------------------------:
![src](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-src-2.jpg) | ![des](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-des-2.png) | ![visual](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-visual-2.png)

Ta cũng có thể sử dụng kỹ thuật trên cho các loại ảnh khác nhau!

Source                     | Edge Detection             | Visualization
:-------------------------:|:-------------------------: | :-------------------------:
![src](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-src.jpg) | ![des](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-des.png) | ![visual](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-visual.png)

## 2.1. Phân vùng

Khi xử lý một hình ảnh chụp văn bản, khu vực văn bản trong bức ảnh ấy sẽ có nhiều sự biến thiên hơn so với các khu vực còn lại. Dựa vào điều này, ta có thể phân vùng bức ảnh, cắt bức ảnh sao cho chỉ còn lại những thông tin cần thiết, và tiến vào những bước xử lý tiếp theo.

Source                     | Sobel Operator             | Segmentation
:-------------------------:|:-------------------------: | :-------------------------:
![src](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/segment-src.png) | ![des](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/segment-sobel.png) | ![visual](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/segment-des.png)

Ta có nhiều cách để biết được vùng nào là văn bản, vùng nào là nền nhằm tiến hành phân vùng như trên. Riêng đối với bài viết này, tác giả sử dụng [Normalization](https://en.wikipedia.org/wiki/Normalization_(statistics)) như sau:

Ta cắt ảnh *Sobel Operator* ở bảng trên ra thành nhiều khối vuông nhỏ, mỗi khối có kích thước $N \times N$.

Với mỗi khối $K$, ta có $K_{ij}$ là độ sáng ở hàng $i$ cột $j$ của K, và $a_{K}$ là trung bình độ sáng của các pixel trong $K$. Gọi:

$$S = \sqrt{ \dfrac{ \sum((K_{ij} - a_{K})^2) }{N \times N} }$$

Ta thấy: Nếu một ô vuông có giá trị $S$ lớn thì ô đó có sự biến thiên màu sắc mạnh mẽ, đồng nghĩa với việc có nhiều chữ cái tại ô đó. Ngược lại, ô đó là nền. Ta thử áp dụng công thức tính $S$ lên 3 khối sau:

Block 0                    | Block 1                    | Block 2
:-------------------------:|:-------------------------: | :-------------------------:
![block-0](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/norm-0.png) | ![block-1](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/norm-1.png) | ![block-2](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/norm-2.png)

Sau khi tính $S$, ta có được $S$ của *Block 0* là lớn nhất, rồi đến *Block 1*, rồi bé nhất là *Block 2*.

Để có được những block ít nhiễu hơn 3 block ví dụ, ta có thể lần lượt sử dụng [Erosion](https://homepages.inf.ed.ac.uk/rbf/HIPR2/erode.htm) rồi đến [Dilation](https://homepages.inf.ed.ac.uk/rbf/HIPR2/dilate.htm) trên ảnh *Sobel Operator* ban đầu rồi hẵn cắt ra thành các block.

## 2.2. Vector hóa - Phần 1: Lấy góc của Vector

Kernel được sử dụng trong phần **2.0** sẽ phát hiện sự biến thiên màu sắc theo chiều $Ox$. Nếu ta xoay nó một góc 90 độ, ta sẽ được một Kernel phát hiện sự biến thiên màu sắc theo chiều $Oy$.

Source                     | Ox Gradient                | Oy Gradient
:-------------------------:|:-------------------------: | :-------------------------:
![src](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-src-2.jpg) | ![ox-grad](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-des-2.png) | ![oy-grad](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-des-y-2.png)

Như vậy, tại một điểm $(i, j)$ trong ma trận, ta sẽ tính được 2 vector $\overrightarrow{Gx}, \overrightarrow{Gy}$ lần lượt là độ biến thiên màu sắc theo chiều $Ox$ và $Oy$ tại điểm đó. Sau đó, ta tính được là góc biến thiên tại $(i, j)$ là:

$$\theta = \tan^{-1}\dfrac{|\overrightarrow{Gx}|}{|\overrightarrow{Gy}|}$$

Có được góc biến thiên $\theta$ đồng nghĩa với việc có thêm dữ liệu để dựng lại các chữ cái bằng tổng các vector. Máy tính không thể dễ dàng nhận diện chữ cái từ bitmap, nhưng có thể dễ dàng nhận diện chúng dưới dạng vector. File *.pdf* thực chất cũng chỉ là tổng các vector, song máy tính vẫn có thể dễ dàng thêm/sửa/xóa/tìm kiếm nội dung trên file pdf.

# 3. Biến đổi Affine

## 3.0. Nhận diện góc nghiêng

Khi người dùng chụp ảnh đưa vào phần mềm OCR, phần văn bản của bức ảnh có thể bị nghiêng, gây khó khăn trong quá trình xử lý ảnh. Để khắc phục, ta có thể xoay ảnh một cách nhanh chóng và hiệu quả như sẽ được trình bày sắp tới đây.

Gọi $M$ là ma trận biểu diễn ảnh ban đầu và $Orient$ là ma trận có cùng kích thước với $M$, sao cho:

$$Orient_{ij} = \theta_{M_{ij}}$$

Bằng việc áp dụng các kỹ thuật [data mining](https://en.wikipedia.org/wiki/Data_mining), ta có thể rút ra một số quy luật giữa các góc $\theta$ với nhau. Lấy ví dụ trong hình *Oy Gradient*, ta có một số vị trí có giá trị $\theta$ gần giống nhau như sau:

Oy Gradient                 | Pattern
:-------------------------: | :-------------------------:
![oy-grad](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-des-y-2.png) | ![pattern](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-pattern.png)

Ta có thể coi các giá trị $\theta$ đó là độ nghiêng so với trục hoành hoặc trục tung rồi tiến hành xoay ảnh.

Tất nhiên, cách làm này có thể sẽ không hiệu quả đối với một số loại font chữ. Khi đó, ta có thể sử dụng *advanced sweepline techniques* (thuật toán đường quét) để tìm kiếm độ nghiêng của một hàng bất kỳ, từ đó suy ra độ nghiêng của văn bản. Tuy nhiên, cách làm này sẽ tương đối tốn kém thời gian hơn, chỉ nên được sử dụng nếu cách đầu tiên không hiệu quả.

## 3.1. Xoay ảnh

Lấy trọng tâm của ảnh $M$ làm gốc tọa độ $O$, điểm $(y, x)$ là điểm có giá trị $Oy = y$, $Ox = x$ trong hệ trục tọa độ $Oxy$. Khi ta xoay ảnh $M$ một góc $\alpha$ quanh $O$, thì một điểm $(y, x)$ trong ảnh sẽ dịch chuyển đến vị trí mới $(y', x')$ được tính như sau:

$$y' = x\sin(\alpha) + y\cos(\alpha)$$

$$x' = x\cos(\alpha) - y\sin(\alpha)$$

Áp dụng phép tính ấy trên mọi điểm trong ảnh $M$, ta xoay được ảnh $M$ một góc $\alpha$ theo chiều vòng tròn lượng giác.

Source                      | After Rotation
:-------------------------: | :-------------------------:
![src](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/sobel-src.jpg) | ![pattern](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/affine-0.png)

Lưu ý: Gọi $h, w$ lần lượt là chiều cao và chiều dài của ảnh $M$ ban đầu, ta có kích thước của ảnh sau khi xoay một góc $\alpha$ là:

$$h' = w\sin(\alpha) + h\cos(\alpha)$$

$$w' = w\cos(\alpha) + h\sin(\alpha)$$

Vậy, nếu văn bản ban đầu bị nghiêng một góc $\theta$, ta xoay ảnh một góc $-\theta$ để ảnh không bị nghiêng nữa. Sau đó, ta có thể cắt từng dòng ra rồi xử lý chúng riêng biệt với nhau.

## 3.2. Distortion 

Sau khi xoay ảnh, văn bản vẫn có thể bị cong. Ta cũng có thể khắc phục điều này bằng Distortion với công thức sau:

$$y' = y + (y - c_{y})(K_{1}r^2 + K_{2}r^4 + ...) + (2P_{1}(x - c_{x})(y - c_{y}) + P_{2}(r^2 + 2(y - c_{y})^2))(1 + P_{3}r^2 + P_{4}r^4)$$

$$x' = x + (x - c_{x})(K_{1}r^2 + K_{2}r^4 + ...) + (P_{1}(r^2 + 2(x - c_{x})^2) + 2P_{2}(x - c_{x})(y - c_{y}))(1 + P_{3}r^2 + P_{4}r^4)$$

Trong đó,

- $y, x$ là vị trí ban đầu của pixel
- $y', x'$ là vị trí của pixel ban đầu sau phép biến đổi
- $c_{y}, c_{x}$ là trọng tâm của phép biến đổi
- $K_{n}, P_{n}$ lần lượt là hệ số *radial distortion* và *tangential distortion*, được phổ cập trong [bài viết này](https://ori.codes/artificial-intelligence/camera-calibration/camera-distortions/).
- $r = \sqrt{(x - c_{x})^2 + (y - c_{y})^2}$

Một lần nữa, ta có thể trích xuất các giá trị $K_{n}, P_{n}$ từ ma trận $Orient$ đã tính từ trước bằng các kỹ thuật [data mining](https://en.wikipedia.org/wiki/Data_mining).

## 3.3. Kết luận

Kết hợp ảnh đã phân vùng từ **Chương 2** và kỹ thuật của **Chương 3**, ta lại tiến thêm 1 bước trong quá trình tiền xử lý ảnh:

Segmented Image            | Affine Rotation (Last Row is Straight)    | Distortion
:-------------------------:|:-------------------------: | :-------------------------:
![segment](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/affine-1.png) | ![rotate](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/affine-2.png) | ![distor](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/affine-3.png)

# 4. Dựng lại các chữ cái

Sau quá trình tiền xử lý kể trên, ta có thể cắt ảnh ban đầu ra thành từng dòng và thực hiện xử lý trên từng dòng riêng lẻ. 

## 3.0. Binary Effect

Với mỗi dòng, ta thực hiện Edge Detection đã được đề cập ở trên, rồi lại sử dụng những thuật toán phù hợp để lấy mặt chữ (ở đây, tác giả dùng thuật toán BFS, rồi lại dùng Dilation + Erosion Morphology)

Source                     | Edge Detection             | Unified
:-------------------------:|:-------------------------: | :-------------------------:
![src](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/restruct-0.png) | ![sobel](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/restruct-1.png) | ![unify](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/restruct-2.png)

Cách dựng thông qua Edge Detection tốt hơn cách dựng trực tiếp từ ảnh ban đầu, vì nó không bị phụ thuộc bởi màu sắc của nền hay chữ, cũng không bị ảnh hưởng nếu màu chữ không đều.

Sau khi dựng, ta được một ảnh nhị phân. Những vị trí được đánh số 1 (màu trắng) là chữ cái, còn những vị trí được đánh số 0 (màu đen) là nền.

## 3.1. Skeletonization

Ta có thể tiếp tục đơn giản hóa các chữ cái bằng cách lấy khung của nó như sau:

Unified                     | Skeletonization
:-------------------------: | :-------------------------:
![src](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/restruct-3.png) | ![pattern](https://raw.githubusercontent.com/iluvgirlswithglasses/ComputerVisionDemo/main/sample-images/restruct-4.png)

Với bấy nhiêu đây dữ liệu, ta có thể dễ dàng nhận diện chữ cái *insert abc xyz anything here*
